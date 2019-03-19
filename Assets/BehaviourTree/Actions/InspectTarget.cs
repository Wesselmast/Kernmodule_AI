using System.Collections;
using UnityEngine;

namespace IMBT {
    public class InspectTarget : BTNode {
        private const float approachRange = 1f;
        private bool doneCalculation = false;
        private readonly MonoBehaviour monoBehaviour;

        public InspectTarget(MonoBehaviour monoBehaviour) {
            this.monoBehaviour = monoBehaviour;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!bb.IsInspecting) {
                bb.IsPatrolling = false;
                bb.IsMovingBack = false;
                bb.Inspected = false;
                bb.IsInspecting = true;
                PathRequestManager.RequestPath(new PathRequest(bb.Agent.transform.position, bb.Target.position,
                    (Vector3[] newPath, bool success) => {
                        if (success) {
                            bb.Path = newPath;
                            monoBehaviour.StopAllCoroutines();
                            monoBehaviour.StartCoroutine(DoPath(bb));
                        }
                    }));
            }
            if (doneCalculation) {
                bb.Inspected = true;
                bb.IsInspecting = false;
                doneCalculation = false;
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Failed;
        }

        private IEnumerator DoPath(BlackBoard bb) {
            Vector3 currentWp = bb.Path[0];
            int index = 0;
            while (true) {
                if (Vector3.Distance(bb.Agent.transform.position, currentWp) < approachRange) {
                    index++;
                    if (index >= bb.Path.Length) {
                        doneCalculation = true;
                        yield break;
                    }
                    currentWp = bb.Path[index];
                }
                bb.Agent.transform.position += (currentWp - bb.Agent.transform.position).normalized * bb.Settings.RunSpeed * Time.deltaTime;
                Quaternion lookRot = Quaternion.LookRotation((currentWp - bb.Agent.transform.position).normalized);
                bb.Agent.transform.rotation = Quaternion.Slerp(bb.Agent.transform.rotation, lookRot, Time.deltaTime * bb.Settings.TurnSpeed);
                yield return null;
            }
        }
    }
}