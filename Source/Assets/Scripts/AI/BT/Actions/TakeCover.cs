using System.Collections;
using UnityEngine;

namespace IMBT {
    public class TakeCover : BTNode {
        private MonoBehaviour monoBehaviour;

        public TakeCover(MonoBehaviour monoBehaviour) {
            this.monoBehaviour = monoBehaviour;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!foundPath) {
                Collider point = bb.GetValue<Collider>("NearestCoverPoint");
                PathRequestManager.RequestPath(new PathRequest(bb.GetValue<GameObject>("Agent").transform.position, point.transform.position,
                        (Vector3[] newPath, bool success) => {
                            foundPath = success;
                            if (foundPath) {
                                bb.SetValue("Path", newPath);

                                monoBehaviour.StopAllCoroutines();
                                bb.SetValue("CurrentPathEnumeration", DoPath(bb));
                                monoBehaviour.StartCoroutine(bb.GetValue<IEnumerator>("CurrentPathEnumeration"));
                            }
                        }));
            }
            if (bb.GetValue<IEnumerator>("CurrentPathEnumeration") == null && foundPath) {
                foundPath = false;
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Running;
        }
    }
}
