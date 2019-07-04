using UnityEngine;
using System.Collections;

namespace IMBT {
    public class DoAPatrol : BTNode {
        private readonly MonoBehaviour monoBehaviour;

        public DoAPatrol(MonoBehaviour monoBehaviour) {
            this.monoBehaviour = monoBehaviour;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (bb.GetValue<IEnumerator>("CurrentPathEnumeration") == null) {
                if (bb.GetValue<int>("Patrol Index") > bb.GetValue<WaypointCollection>("Patrol Path").GetWaypoints().Length - 1) {
                    bb.SetValue("Patrol Index", 0);
                }
                PathRequestManager.RequestPath(new PathRequest(bb.GetValue<GameObject>("Agent").transform.position, 
                            bb.GetValue<WaypointCollection>("Patrol Path").GetWaypoints()[bb.GetValue<int>("Patrol Index")],
                    (Vector3[] newPath, bool success) => {
                        if (success) {
                            bb.SetValue("Patrol Index", bb.GetValue<int>("Patrol Index") + 1);
                            bb.SetValue("Path", newPath);

                            monoBehaviour.StopAllCoroutines();

                            bb.SetValue("CurrentPathEnumeration", DoPath(bb));
                            monoBehaviour.StartCoroutine(bb.GetValue<IEnumerator>("CurrentPathEnumeration"));
                        }
                    }));
            }
            return BTTaskStatus.Running;
        }
    }
}