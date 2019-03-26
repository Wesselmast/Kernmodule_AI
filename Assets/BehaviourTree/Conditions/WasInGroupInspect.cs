namespace IMBT {
    public class WasInGroupInspect : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            if (bb.GetValue<bool>("WasInGroupInspect")) return BTTaskStatus.Success;
            return BTTaskStatus.Failed;
        }
    }
}