using UnityEngine;
using System.Collections;

namespace IMBT {
    public class MoveBackToOldSpot : BTNode {
        private readonly MonoBehaviour monoBehaviour;

        public MoveBackToOldSpot(MonoBehaviour monoBehaviour) {
            this.monoBehaviour = monoBehaviour;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!foundPath) {
                PathRequestManager.RequestPath(new PathRequest(bb.GetValue<GameObject>("Agent").transform.position, bb.GetValue<Vector3>("OldSpot"),
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
                bb.SetValue("OldSpotSaved", false);
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Running;
        }
    }
}