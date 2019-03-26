using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable {
    private float currentHealth;

    private void Start() {
        currentHealth = GetComponent<ComplexEnemy>().BlackBoard.Settings.Health;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    public void Die() {
        Destroy(gameObject);
    }
}
