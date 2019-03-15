using UnityEngine;

[System.Serializable]
public struct WeaponMoodSettings : IMoodSettings {
    [SerializeField] private float attackSpeed;
    public float AttackSpeed { get { return 1f / attackSpeed; } }
    [SerializeField] private float attackDamage;
    public float AttackDamage { get { return attackDamage; } }
    [SerializeField] private float maxAmmo;
    public float MaxAmmo { get { return maxAmmo; } }
    [SerializeField] private float bulletLifeTime;
    public float BulletLifeTime { get { return bulletLifeTime; } }
    [SerializeField] private float meleeRange;
    public float MeleeRange { get { return meleeRange; } }
    [SerializeField] private float bulletTravelSpeed;
    public float BulletTravelSpeed { get { return bulletTravelSpeed; } }
    [SerializeField] private float bulletSpread;
    public float BulletSpread { get { return bulletSpread; } }
    [SerializeField] private AudioClip attackSound;
    public AudioClip AttackSound { get { return attackSound; } }
    [SerializeField] private bool isMelee;
    public bool IsMelee { get { return isMelee; } }
}