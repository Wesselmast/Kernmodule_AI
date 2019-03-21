using System.Collections;
using UnityEngine;

namespace IMBT {
    public class DoAPatrol : BTNode {
        private readonly MonoBehaviour monoBehaviour;
        private const float approachRange = .3f;
        private int patrolIndex = 0;

        public DoAPatrol(MonoBehaviour monoBehaviour) {
            this.monoBehaviour = monoBehaviour;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!bb.IsPatrolling) {
                bb.IsPatrolling = true;
                bb.IsInspecting = false;
                bb.IsMovingBack = false;
                if (patrolIndex > bb.PatrolPath.GetWaypoints().Count - 1) patrolIndex = 0;
                PathRequestManager.RequestPath(new PathRequest(bb.Agent.transform.position, bb.PatrolPath.GetWaypoints()[patrolIndex].position,
                    (Vector3[] newPath, bool success) => {
                        if (success) {
                            patrolIndex++;
                            bb.Path = newPath;
                            monoBehaviour.StopAllCoroutines();
                            monoBehaviour.StartCoroutine(DoPath(bb));
                        }
                    }));
            }
            return BTTaskStatus.Running;
        }

        private IEnumerator DoPath(BlackBoard bb) {
            Vector3 currentWp = bb.Path[0];
            int index = 0;
            while (true) {
                if (Vector3.Distance(bb.Agent.transform.position, currentWp) < approachRange) {
                    index++;
                    if (index >= bb.Path.Length) {
                        bb.IsPatrolling = false;
                        yield break;
                    }
                    currentWp = bb.Path[index];
                }
                bb.Agent.transform.position += (currentWp - bb.Agent.transform.position).normalized * bb.Speed * Time.deltaTime;
                Quaternion lookRot = Quaternion.LookRotation((currentWp - bb.Agent.transform.position).normalized);
                bb.Agent.transform.rotation = Quaternion.Slerp(bb.Agent.transform.rotation, lookRot, Time.deltaTime * bb.Settings.TurnSpeed);
                yield return null;
            }
        }
    }
}