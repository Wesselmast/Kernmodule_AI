namespace IMBT {
    public class BTSequence : BTNodeBase {
        private BTNodeBase[] nodes;
        public BTSequence(params BTNodeBase[] nodes) {
            this.nodes = nodes;
        }

        public override TaskStatus Tick(BlackBoard bb) {
            foreach (BTNodeBase b in nodes) {
                TaskStatus status = b.Tick(bb);
                switch (status) {
                    case TaskStatus.Failed: return TaskStatus.Failed;
                    case TaskStatus.Running: return TaskStatus.Running;
                    case TaskStatus.Success: continue;
                }
            }
            return TaskStatus.Success;
        }
    }
}