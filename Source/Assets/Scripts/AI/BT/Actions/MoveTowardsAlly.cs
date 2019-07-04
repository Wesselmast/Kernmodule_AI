using System.Collections;
using UnityEngine;

namespace IMBT {
    public class MoveTowardsAlly : BTNode {
        private readonly Enemy me;
        private float elapsed = 0;

        public MoveTowardsAlly(Enemy me) {
            this.me = me;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            elapsed += Time.deltaTime;
            if (elapsed > 0.5f) {
                Enemy ally = bb.GetValue<Enemy>("NearestAlly");
                PathRequestManager.RequestPath(new PathRequest(bb.GetValue<GameObject>("Agent").transform.position,
                                                               ally.transform.position,
                    (Vector3[] newPath, bool success) => {
                        if (success) {
                            elapsed = 0;
                            bb.SetValue("Path", newPath);

                            ally.BlackBoard.SetValue("NearestAlly", me);
                            ally.BlackBoard.SetValue("MoveTowardsAlly", true);

                            me.StopAllCoroutines();
                            bb.SetValue("CurrentPathEnumeration", DoPath(bb));
                            me.StartCoroutine(bb.GetValue<IEnumerator>("CurrentPathEnumeration"));
                        }
                    }));
            }
            else if (bb.GetValue<IEnumerator>("CurrentPathEnumeration") == null) {
                foundPath = false;
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Running;
        }
    }
}