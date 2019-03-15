namespace VaalsBT {
    public class BTInvert : BTNodeBase {
        private BTNodeBase node;
        public BTInvert(BTNodeBase node) {
            this.node = node;
        }
        
        public override TaskStatus Tick(BlackBoard bb) {
            TaskStatus status = node.Tick(bb);
            switch (status) {
                case TaskStatus.Failed: return TaskStatus.Success;
                case TaskStatus.Running: return TaskStatus.Running;
                case TaskStatus.Success: return TaskStatus.Failed;
            }
            return TaskStatus.Success;
        }
    }
}