using UnityEngine;

namespace IMBT {
    public class TargetInShootableRange : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            if (bb.GetValue<EnemyFOV>("FOV").GetShootableTarget(bb.GetValue<Transform>("Target")) != null) return BTTaskStatus.Success;
            return BTTaskStatus.Failed;
        }
    }
}
