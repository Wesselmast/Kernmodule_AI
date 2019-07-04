using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : PlayerBehaviour, IHealth {
    [SerializeField] private Slider healthSlider = default;
    private float currentHealth = 0;

    protected override void Begin() {
        currentHealth = settings.Health;
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0) Die();
    }

    public void Die() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}