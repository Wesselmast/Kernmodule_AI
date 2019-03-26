using UnityEngine;

namespace IMBT {
    public class SaveOldSpot : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            if (!bb.GetValue<bool>("OldSpotSaved")) {
                bb.SetValue("OldSpot", bb.GetValue<GameObject>("Agent").transform.position);
                bb.SetValue("OldSpotSaved", true);
            }
            return BTTaskStatus.Success;
        }
    }
}