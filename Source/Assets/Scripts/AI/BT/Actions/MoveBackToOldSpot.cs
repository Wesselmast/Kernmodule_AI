using UnityEngine;
using System.Collections;

namespace IMBT {
    public class MoveBackToOldSpot : BTNode {
        private const float approachRange = .5f;
        private bool doneCalculation = false;
        private readonly MonoBehaviour monoBehaviour;

        public MoveBackToOldSpot(MonoBehaviour monoBehaviour) {
            this.monoBehaviour = monoBehaviour;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!bb.GetValue<bool>("IsMovingBack")) {
                doneCalculation = false;
                bb.SetValue("IsMovingBack", true);
                PathRequestManager.RequestPath(new PathRequest(bb.GetValue<GameObject>("Agent").transform.position, bb.GetValue<Vector3>("OldSpot"),
                    (Vector3[] newPath, bool success) => {
                        if (success) {
                            bb.SetValue("Path", newPath);
                            monoBehaviour.StopAllCoroutines();
                            monoBehaviour.StartCoroutine(DoPath(bb));
                        }
                    }));
            }
            if (doneCalculation) {
                bb.SetValue("OldSpotSaved", false);
                bb.SetValue("IsInspecting", false);
                bb.SetValue("IsMovingTowardsShrine", false);
                bb.SetValue("IsPatrolling", false);
                if (bb.GetValue<BTState>("State") == BTState.GroupInspect) {
                    bb.SetValue("IsGroupInspecting", false);
                    bb.SetValue("WasInGroupInspect", true);

                }
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