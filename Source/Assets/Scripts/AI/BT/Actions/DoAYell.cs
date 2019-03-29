namespace IMBT {
    public class DoAYell : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            foreach(var a in bb.GetValue<EnemyFOV>("FOV").GetAlliesInCommunicationRadius()) {
                if (a.BlackBoard.GetValue<BTState>("State") == BTState.Combat) {
                    a.BlackBoard.SetValue("State", BTState.Combat);
                }
                else {
                    a.BlackBoard.SetValue("State", BTState.GroupInspect);
                }
            }
            return BTTaskStatus.Success;
        }
    }
}