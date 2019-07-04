using UnityEngine;

namespace IMBT {
    public class ShootPlayer : BTNode {
        private bool firstTime = true;
        private Weapon weapon = null;
        private float elapsed = 0;

        public override BTTaskStatus Tick(BlackBoard bb) {
            if (firstTime) {
                weapon = bb.GetValue<GameObject>("Agent").GetComponentInChildren<Weapon>();
                firstTime = false;
            }
            elapsed += Time.deltaTime;
            Transform target = bb.GetValue<Transform>("Target");
            bb.GetValue<GameObject>("Agent").transform.LookAt(target);
            if (elapsed > weapon.Settings.AttackSpeed) {
                IHealth health = target.GetComponent<IHealth>();
                AudioSource source = weapon.GetComponent<AudioSource>() ?? weapon.gameObject.AddComponent<AudioSource>();
                source.Play();
                if (health != null) health.TakeDamage(weapon.Settings.AttackDamage);
                elapsed = 0;
            }
            return BTTaskStatus.Running;
        }
    }
}
