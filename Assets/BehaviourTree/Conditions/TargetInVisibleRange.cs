using UnityEngine;

namespace IMBT {
    public class TargetInVisibleRange : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            Transform target = bb.Fov.GetSeeableTarget(bb.Target);
            if (target != null) {
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Failed;
        }
    }
}