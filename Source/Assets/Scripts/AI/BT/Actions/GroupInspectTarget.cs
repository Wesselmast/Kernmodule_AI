using System.Collections;
using UnityEngine;

namespace IMBT {
    public class GroupInspectTarget : BTNode {
        private const float approachRange = 1f;
        private readonly MonoBehaviour monoBehaviour;
        private bool doneCalculation = false;

        public GroupInspectTarget(MonoBehaviour monoBehaviour) {
            this.monoBehaviour = monoBehaviour;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!bb.GetValue<bool>("IsGroupInspecting")) {
                doneCalculation = false;
                bb.SetValue("IsGroupInspecting", true);
                bb.SetValue("IsInspecting", true);
                bb.SetValue("IsPatrolling", false);
                bb.SetValue("IsMovingBack", false);
                bb.SetValue("IsTakingCover", false);
                PathRequestManager.RequestPath(new PathRequest(bb.GetValue<GameObject>("Agent").transform.position, bb.GetValue<Transform>("Target").position,
                    (Vector3[] newPath, bool success) => {
                        if (success) {
                            bb.SetValue("Speed", bb.Settings.RunSpeed);
                            bb.SetValue("Path", newPath);
                            monoBehaviour.StopAllCoroutines();
                            monoBehaviour.StartCoroutine(DoPath(bb));
                        }
                    }));
            }
            if (doneCalculation) {
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Running;
        }

        private IEnumerator DoPath(BlackBoard bb) {
            Vector3[] path = bb.GetValue<Vector3[]>("Path");
            GameObject agent = bb.GetValue<GameObject>("Agent");
            Vector3 currentWp = path[0];
            int index = 0;
            while (true) {
                if (Vector3.Distance(agent.transform.position, currentWp) < approachRange) {
                    index++;
                    if (index >= path.Length) {
                        doneCalculation = true;
                        yield break;
                    }
                    currentWp = path[index];
                }
                agent.transform.position += (currentWp - agent.transform.position).normalized * bb.GetValue<float>("Speed") * Time.deltaTime;
                Quaternion lookRot = Quaternion.LookRotation((currentWp - agent.transform.position).normalized);
                agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRot, Time.deltaTime * bb.Settings.TurnSpeed);
                yield return null;
            }
        }
    }
}