using UnityEngine;

public class Bullet : MonoBehaviour {
    public float Damage { get => damage; set => damage = value; }
    public float Lifetime { get => lifetime; set => lifetime = value; }
    public float BulletTravelSpeed { get => bulletTravelSpeed; set => bulletTravelSpeed = value; }

    private float lifetime = 0;
    private float damage = 0;
    private float bulletTravelSpeed = 0;

    private void Update() {
        if (lifetime <= 0) BulletPool.Instance.ReturnToPool(this);
        else lifetime -= Time.deltaTime;
        Vector3 lastPosition = transform.position;
        transform.position += transform.forward * bulletTravelSpeed * Time.deltaTime;
        if (Physics.Linecast(lastPosition, transform.position, out RaycastHit hit)) {
            Collider hitCollider = hit.collider;
            if (hitCollider != null && hitCollider.GetComponent<Player>() == null) {
                IHealth health = hitCollider.GetComponentInParent<IHealth>();
                if (health != null) {
                    health.TakeDamage(damage);
                }
            }
            BulletPool.Instance.ReturnToPool(this);
        }
    } 
}
