using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable {
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    public void Die() {
        Destroy(gameObject);
    }
}
