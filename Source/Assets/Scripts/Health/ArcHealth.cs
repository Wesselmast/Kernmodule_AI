using UnityEngine;
using UnityEngine.UI;

public class ArcHealth : MonoBehaviour, IHealth {
    [SerializeField] private Slider healthSlider = default;
    [SerializeField] private float maxHealth = 0;
    private float currentHealth = 0;

    private void Awake() {
        healthSlider = GetComponentInChildren<Slider>();
    }

    private void Start() {
        currentHealth = maxHealth;
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