using UnityEngine;
using System.Linq;

namespace IMBT {
    public class GetNearestCoverPoint : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            float nearest = float.MaxValue;
            Collider nearestCoverPoint = null;
            foreach (var c in bb.Fov.GetCoversInRange()
                        .Where(cover => Physics.Linecast(cover.transform.position, bb.Target.position, LayerMask.GetMask("Obstacle")))) {
                float dist = Vector3.Distance(bb.Agent.transform.position, c.transform.position);
                if (dist < nearest) {
                    nearest = dist;
                    nearestCoverPoint = c;
                }
            }
            if (nearestCoverPoint != null) {
                bb.NearestCoverPoint = nearestCoverPoint;
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Failed;
        }
    }
}