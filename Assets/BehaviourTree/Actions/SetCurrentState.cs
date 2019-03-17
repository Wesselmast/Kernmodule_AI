namespace IMBT {
    public class SetCurrentState : BTNode {
        private BTState state;

        public SetCurrentState(BTState state) {
            this.state = state;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (state == BTState.GroupInspect) bb.WasInGroupInspect = true;
            bb.State = state;
            return BTTaskStatus.Success;
        }
    }
}