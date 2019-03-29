namespace IMBT {
    public class BTSequence : BTNode {
        private BTNode[] nodes;
        public BTSequence(params BTNode[] nodes) {
            this.nodes = nodes;
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            foreach (BTNode b in nodes) {
                BTTaskStatus status = b.Tick(bb);
                switch (status) {
                    case BTTaskStatus.Failed: return BTTaskStatus.Failed;
                    case BTTaskStatus.Running: return BTTaskStatus.Running;
                    case BTTaskStatus.Success: continue;
                }
            }
            return BTTaskStatus.Success;
        }
    }
}