using System.Collections;
using UnityEngine;

namespace IMBT {
    public class InspectTarget : BTNode {
        private readonly MonoBehaviour monoBehaviour;

        public InspectTarget(MonoBehaviour monoBehaviour) {
            this.monoBehaviour = monoBehaviour;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!foundPath) {
                PathRequestManager.RequestPath(new PathRequest(bb.GetValue<GameObject>("Agent").transform.position, bb.GetValue<Transform>("Target").position,
                    (Vector3[] newPath, bool success) => {
                        foundPath = success;
                        if (foundPath) {
                            if (this == null) return;
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