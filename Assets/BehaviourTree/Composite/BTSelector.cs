namespace IMBT {
    public class BTSelector : BTNodeBase {
        private BTNodeBase[] nodes;
        public BTSelector(params BTNodeBase[] nodes) {
            this.nodes = nodes;
        }

        public override TaskStatus Tick(BlackBoard bb) {
            foreach (BTNodeBase b in nodes) {
                TaskStatus status = b.Tick(bb);
                switch (status) {
                    case TaskStatus.Failed: continue;
                    case TaskStatus.Running: return TaskStatus.Running;
                    case TaskStatus.Success: return TaskStatus.Success;
                }
            }
            return TaskStatus.Failed;
        }
    }
}

