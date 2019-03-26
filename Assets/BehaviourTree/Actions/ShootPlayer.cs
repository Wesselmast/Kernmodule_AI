using UnityEngine;

namespace IMBT {
    public class ShootPlayer : BTNode {
        private bool done = false;
        private Weapon weapon = null;

        public override BTTaskStatus Tick(BlackBoard bb) {
            if(!done) {
                weapon = bb.GetValue<GameObject>("Agent").GetComponentInChildren<Weapon>();
            }
            Transform target = bb.GetValue<Transform>("Target");
            bb.GetValue<GameObject>("Agent").transform.LookAt(target);
            weapon.Attack(target.position);
            return BTTaskStatus.Running;
        }
    }
}
