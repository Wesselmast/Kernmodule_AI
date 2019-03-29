using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamagable {
    private float currentHealth;
    private Slider healthSlider;

    private void Awake() {
        currentHealth = GetComponent<SearcherEnemy>().BlackBoard.Settings.Health;
        healthSlider = GetComponentInChildren<Slider>();
    }

    private void Start() {
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage) {
        healthSlider.value = currentHealth;
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    public void Die() {
        Destroy(gameObject);
    }
}