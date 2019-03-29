using UnityEngine;
using System.Linq;

namespace IMBT {
    public class GetNearestCoverPoint : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            float nearest = float.MaxValue;
            Collider nearestCoverPoint = null;
            foreach (var c in bb.GetValue<EnemyFOV>("FOV").GetCoversInRange()
                        .Where(cover => Physics.Linecast(cover.transform.position, bb.GetValue<Transform>("Target").position, LayerMask.GetMask("Obstacle")))) {
                if (c.gameObject == bb.GetValue<GameObject>("Agent")) continue;
                float dist = Vector3.Distance(bb.GetValue<GameObject>("Agent").transform.position, c.transform.position);
                if (dist < nearest) {
                    nearest = dist;
                    nearestCoverPoint = c;
                }
            }
            if (nearestCoverPoint != null) {
                bb.SetValue("NearestCoverPoint", nearestCoverPoint);
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Failed;
        }
    }
}