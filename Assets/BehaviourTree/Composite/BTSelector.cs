namespace IMBT {
    public class BTSelector : BTNode {
        private BTNode[] nodes;
        public BTSelector(params BTNode[] nodes) {
            this.nodes = nodes;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            foreach (BTNode b in nodes) {
                BTTaskStatus status = b.Tick(bb);
                switch (status) {
                    case BTTaskStatus.Failed: continue;
                    case BTTaskStatus.Running: return BTTaskStatus.Running;
                    case BTTaskStatus.Success: return BTTaskStatus.Success;
                }
            }
            return BTTaskStatus.Failed;
        }
    }
}

