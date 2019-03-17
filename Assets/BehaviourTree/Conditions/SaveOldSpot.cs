namespace IMBT {
    public class SaveOldSpot : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!bb.OldSpotSaved) {
                bb.OldSpot = bb.Agent.transform.position;
                bb.OldSpotSaved = true;
            }
            return BTTaskStatus.Success;
        }
    }
}