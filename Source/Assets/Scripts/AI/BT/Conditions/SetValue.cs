namespace IMBT {
    public class SetValue<T> : BTNode {

        private string name = string.Empty;
        private T to = default;

        public SetValue(string name, T to) {
            this.name = name;
            this.to = to;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            bb.SetValue(name, to);
            return BTTaskStatus.Success;
        }
    }
}