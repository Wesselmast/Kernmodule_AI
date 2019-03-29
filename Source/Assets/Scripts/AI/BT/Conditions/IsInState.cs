namespace IMBT {
    public class IsInState : BTNode {
        private BTState state;

        public IsInState(BTState state) {
            this.state = state;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            return bb.GetValue<BTState>("State") == state ? BTTaskStatus.Success : BTTaskStatus.Failed;
        }
    }
}