namespace IMBT {
    public class DoAYell : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            foreach(var a in bb.Fov.GetAlliesInCommunationRadius()) {
                a.blackBoard.State = BTState.GroupInspect;
            }
            return BTTaskStatus.Success;
        }
    }
}