using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour {
    [SerializeField] private WeaponSettings settings;

    private AudioSource source;
    private Animator anim;
    private Transform attackPoint;
    private float attackSpeedElapsed;

    public WeaponSettings Settings {
        get {
            return settings;
        }
        set {
            settings = value;
        }
    }

    void Awake() {
        foreach (var t in GetComponentsInChildren<Transform>()) {
            if (t.name == "AttackPoint") {
                attackPoint = t;
                break;
            }
        }
        source = GetComponent<AudioSource>();
        source.clip = Settings.AttackSound;
        anim = GetComponentInChildren<Animator>(true);
        attackSpeedElapsed = Settings.AttackSpeed;
    }

    private void Update() {
        attackSpeedElapsed += Time.deltaTime;
    }

    public void Attack(Vector3 target) {
        if (attackSpeedElapsed > Settings.AttackSpeed) {
            source.Play();
            if (Settings.IsMelee) Slice(new Ray(attackPoint.position, target));
            else Shoot();
            attackSpeedElapsed = 0;
        }
    }

    public void Attack(Ray ray) {
        if (attackSpeedElapsed > Settings.AttackSpeed) {
            source.Play();
            if (Settings.IsMelee) Slice(ray);
            else Shoot();
            attackSpeedElapsed = 0;
        }
    }

    private void Slice(Ray ray) {
        if (anim != null) anim.Play("Slice");
        source.PlayOneShot(Settings.AttackSound);
        Vector3 hitPoint = Physics.Raycast(ray, out RaycastHit hit, Settings.MeleeRange) ?
                    hit.point : ray.origin + ray.direction * Settings.MeleeRange;
        Collider hitCollider = hit.collider;
        if (hitCollider == null) return;
        IHealth health = hitCollider.GetComponentInParent<IHealth>();
        if (health == null) return;
        health.TakeDamage(Settings.AttackDamage);
    }

    private void Shoot() {
        if (anim != null) anim.Play("Shoot");
        source.PlayOneShot(Settings.AttackSound);
        Bullet bullet = BulletPool.Instance.Get();
        bullet.Lifetime = settings.BulletLifeTime;
        bullet.Damage = settings.AttackDamage;
        bullet.BulletTravelSpeed = settings.BulletTravelSpeed;
        bullet.transform.position = attackPoint.position;
        bullet.transform.rotation = attackPoint.rotation;
        float rand = Random.Range(-Settings.BulletSpread, Settings.BulletSpread);
        bullet.transform.Rotate(new Vector3(rand, rand, rand));
    }
}