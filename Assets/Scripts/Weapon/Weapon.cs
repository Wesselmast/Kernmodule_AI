using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour {
    [SerializeField] private WeaponSettings globalSettings;

    private AudioSource source;
    private Animator anim;
    private Transform attackPoint;
    private float attackSpeedElapsed;

    public WeaponSettings GlobalSettings {
        get {
            return globalSettings;
        }
        set {
            globalSettings = value;
        }
    }

    public WeaponMoodSettings Settings {
        get {
            return (WeaponMoodSettings)globalSettings.GetMoodSettings(MindStateManager.State);
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
        anim.Play("Slice");
        Vector3 hitPoint = Physics.Raycast(ray, out RaycastHit hit, Settings.MeleeRange) ?
                    hit.point : ray.origin + ray.direction * Settings.MeleeRange;
        try { hit.collider.GetComponentInParent<IDamagable>().TakeDamage(Settings.AttackDamage); }
        catch { }
    }

    private void Shoot() {
        anim.Play("Shoot");
        Bullet bullet = BulletPool.Instance.Get();
        bullet.transform.position = attackPoint.position;
        bullet.transform.rotation = attackPoint.rotation;
        float rand = Random.Range(-Settings.BulletSpread, Settings.BulletSpread);
        bullet.transform.Rotate(new Vector3(rand,rand,rand));
    }
}