namespace IMBT {
    public class SetCurrentState : BTNode {
        private BTState state;

        public SetCurrentState(BTState state) {
            this.state = state;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            bb.SetValue("State", state);
            return BTTaskStatus.Success;
        }
    }
}