namespace IMBT {
    public class DoAYell : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            foreach(var a in bb.GetValue<EnemyFOV>("FOV").GetAlliesInCommunationRadius()) {
                a.BlackBoard.SetValue("State", BTState.GroupInspect);
            }
            return BTTaskStatus.Success;
        }
    }
}