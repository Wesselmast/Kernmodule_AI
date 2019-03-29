using System.Linq;
using UnityEngine;

namespace IMBT {
    public class FindNearestShrine : BTNode {
        public override BTTaskStatus Tick(BlackBoard bb) {
            float nearest = float.MaxValue;
            Collider nearestShrine = null;
            EnemyFOV fov = bb.GetValue<EnemyFOV>("FOV");
            foreach (var s in fov.GetShrinesInCommunicationRadius().Where(s => fov.GetSeeableTarget(s.transform))) {
                float dist = Vector3.Distance(bb.GetValue<GameObject>("Agent").transform.position, s.transform.position);
                if (dist < nearest) {
                    nearest = dist;
                    nearestShrine = s.GetComponent<Collider>();
                }
            }
            if (nearestShrine != null) {
                bb.SetValue("NearestVisibleShrine", nearestShrine);
                return BTTaskStatus.Success;
            }
            return BTTaskStatus.Failed;
        }
    }
}