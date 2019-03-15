namespace VaalsBT {
    public abstract class BTNodeBase {
        public virtual void OnInitialize(BlackBoard bb) { }
        public abstract TaskStatus Tick(BlackBoard bb);
    }
}