using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Weapon", fileName = "New Weapon")]
public class WeaponSettings : ScriptableObject {
    [SerializeField] private float attackSpeed = 5f;
    public float AttackSpeed { get { return 1f / attackSpeed; } }
    [SerializeField] private float attackDamage = 10f;
    public float AttackDamage { get { return attackDamage; } }
    [SerializeField] private float maxAmmo = 20f;
    public float MaxAmmo { get { return maxAmmo; } }
    [SerializeField] private float bulletLifeTime = 10f;
    public float BulletLifeTime { get { return bulletLifeTime; } }
    [SerializeField] private float meleeRange = 5f;
    public float MeleeRange { get { return meleeRange; } }
    [SerializeField] private float bulletTravelSpeed = 100f;
    public float BulletTravelSpeed { get { return bulletTravelSpeed; } }
    [SerializeField] private float bulletSpread = 5f;
    public float BulletSpread { get { return bulletSpread; } }
    [SerializeField] private AudioClip attackSound = null;
    public AudioClip AttackSound { get { return attackSound; } }
    [SerializeField] private bool isMelee = false;
    public bool IsMelee { get { return isMelee; } }
}