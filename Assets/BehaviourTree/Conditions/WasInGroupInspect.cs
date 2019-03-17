namespace IMBT {
    public class WasInGroupInspect : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            if (bb.WasInGroupInspect) return BTTaskStatus.Success;
            return BTTaskStatus.Failed;
        }
    }
}