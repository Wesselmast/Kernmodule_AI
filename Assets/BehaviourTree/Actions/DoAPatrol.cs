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
            if (!bb.GetValue<bool>("IsPatrolling")) {
                bb.SetValue("IsInspecting", false);
                bb.SetValue("IsPatrolling", true);
                bb.SetValue("IsMovingBack", false);
                if (patrolIndex > bb.GetValue<WaypointCollection>("Patrol Path").GetWaypoints().Count - 1) patrolIndex = 0;
                PathRequestManager.RequestPath(new PathRequest(bb.GetValue<GameObject>("Agent").
                             transform.position, bb.GetValue<WaypointCollection>("Patrol Path").
                                                           GetWaypoints()[patrolIndex].position,
                    (Vector3[] newPath, bool success) => {
                        if (success) {
                            patrolIndex++;
                            bb.SetValue("Path", newPath);
                            monoBehaviour.StopAllCoroutines();
                            monoBehaviour.StartCoroutine(DoPath(bb));
                        }
                    }));
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
                        bb.SetValue("IsPatrolling", false);
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