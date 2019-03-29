namespace IMBT {
    public class SetBool : BTNode {

        private string name = string.Empty;
        private bool to = false;

        public SetBool(string name, bool to) {
            this.name = name;
            this.to = to;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            bb.SetValue(name, to);
            return BTTaskStatus.Success;
        }
    }
}