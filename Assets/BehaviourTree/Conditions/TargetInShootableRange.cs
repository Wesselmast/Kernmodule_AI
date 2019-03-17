namespace IMBT {
    public class TargetInShootableRange : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            if (bb.Fov.GetShootableTarget(bb.Target) != null) return BTTaskStatus.Success;
            return BTTaskStatus.Failed;
        }
    }
}
