using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IHealth {
    private SearcherEnemy enemy;
    private Slider healthSlider = default;
    private float currentHealth = 0;

    private void Awake() {

        enemy = GetComponent<SearcherEnemy>();
        currentHealth = enemy.BlackBoard.Settings.Health;
        healthSlider = GetComponentInChildren<Slider>();
    }

    private void Start() {
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage) {
        enemy.BlackBoard.SetValue("State", IMBT.BTState.GroupInspect);
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0) Die();
    }

    public void Die() {
        Destroy(gameObject);
    }
}