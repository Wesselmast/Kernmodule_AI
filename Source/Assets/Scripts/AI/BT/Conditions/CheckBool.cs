namespace IMBT {
    public class CheckBool : BTNode {

        private string name = string.Empty;
        private bool b = true;

        public CheckBool(string name) {
            this.name = name;
        }

        public CheckBool(string name, bool b) {
            this.name = name;
            this.b = b;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            return bb.GetValue<bool>(name) == b ? BTTaskStatus.Success : BTTaskStatus.Failed;
        }
    }
}