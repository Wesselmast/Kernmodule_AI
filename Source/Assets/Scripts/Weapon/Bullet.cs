using UnityEngine;

public class Bullet : MonoBehaviour {
    private Weapon weapon;
    private float lifeTime;

    private void Awake() {
        weapon = FindObjectOfType<Weapon>();
        lifeTime = weapon.Settings.BulletLifeTime;
    }

    private void Update() {
        if (lifeTime <= 0) BulletPool.Instance.ReturnToPool(this);
        else lifeTime -= Time.deltaTime;
        Vector3 framePos = transform.position;
        transform.position += transform.forward * weapon.Settings.BulletTravelSpeed * Time.deltaTime;
        if (Physics.Linecast(framePos, transform.position, out RaycastHit hit)) {
            try { hit.collider.GetComponentInParent<IDamagable>().TakeDamage(weapon.Settings.AttackDamage); }
            catch { }
            BulletPool.Instance.ReturnToPool(this);
        }
    }
}
