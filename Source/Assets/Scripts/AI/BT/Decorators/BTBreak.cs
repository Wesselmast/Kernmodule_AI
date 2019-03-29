namespace IMBT {
    public class BTBreak : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            return BTTaskStatus.Failed;
        }
    }
}