using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMBT {
    public class TakeCover : BTNode {
        private MonoBehaviour monoBehaviour;
        private bool doneCalculation = false;
        private const float approachRange = 1f;

        public TakeCover(MonoBehaviour monoBehaviour) {
            this.monoBehaviour = monoBehaviour;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!bb.GetValue<bool>("IsTakingCover")) {
                bb.SetValue("IsGroupInspecting", false);
                bb.SetValue("WasInGroupInspect", false);
                bb.SetValue("IsTakingCover", true);
                Collider point = bb.GetValue<Collider>("NearestCoverPoint");
                PathRequestManager.RequestPath(new PathRequest(bb.GetValue<GameObject>("Agent").transform.position, point.transform.position,
                        (Vector3[] newPath, bool success) => {
                            if (success) {
                                bb.SetValue("Path", newPath);
                                monoBehaviour.StopAllCoroutines();
                                monoBehaviour.StartCoroutine(DoPath(bb));
                            }
                        }));
            }
            if (doneCalculation) {
                bb.SetValue("IsTakingCover", false);
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
