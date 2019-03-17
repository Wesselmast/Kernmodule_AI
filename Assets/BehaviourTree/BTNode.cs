namespace IMBT {
    public abstract class BTNode {
        public virtual void OnInitialize(BlackBoard bb) { }
        public abstract BTTaskStatus Tick(BlackBoard bb);
    }
}