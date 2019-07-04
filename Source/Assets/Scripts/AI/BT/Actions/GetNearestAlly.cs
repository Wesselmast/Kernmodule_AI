using UnityEngine;

namespace IMBT {
    public class GetNearestAlly : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            float nearest = float.MaxValue;
            Enemy nearestAlly = null;
            foreach (var ally in bb.GetValue<EnemyFOV>("FOV").GetAlliesInCommunicationRadius()) {
                float dist = Vector3.Distance(bb.GetValue<GameObject>("Agent").transform.position, ally.transform.position);
                if (dist < nearest) {
                    nearest = dist;
                    nearestAlly = ally;
                }
            }
            if (nearestAlly != null) {
                bb.SetValue("NearestAlly", nearestAlly);
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Failed;
        }
    }
}