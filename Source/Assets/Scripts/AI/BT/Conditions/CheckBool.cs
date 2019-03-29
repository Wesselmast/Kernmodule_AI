namespace IMBT {
    public class CheckBool : BTNode {

        private string name = string.Empty;

        public CheckBool(string name) {
            this.name = name;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (bb.GetValue<bool>(name)) return BTTaskStatus.Success;
            return BTTaskStatus.Failed;
        }
    }
}