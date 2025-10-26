using ArcherOfGod.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArcherOfGod.UI
{
    [RequireComponent(typeof(Slider))]
    public class UIHealth : MonoBehaviour
    {
        private Slider healthSlider;
        private TextMeshProUGUI healthText;

        [SerializeField] private Health health;

        private void Awake()
        {
            healthSlider = GetComponent<Slider>();
            healthText = GetComponentInChildren<TextMeshProUGUI>();

            if (health == null)
            {
                Debug.LogError("Health reference is not set in UIHealth component.", this);
                return;
            }

            if (healthText == null)
            {
                Debug.LogWarning("No TextMeshProUGUI component found in children of UIHealth.", this);
            }

            health.HealthChanged += UpdateHealthUI;
            UpdateHealthUI(health.CurrentHealth, health.MaxHealth);
        }

        private void OnDestroy()
        {
            if (health != null)
            {
                health.HealthChanged -= UpdateHealthUI;
            }
        }

        private void UpdateHealthUI(int current, int max)
        {
            if (healthSlider != null)
            {
                healthSlider.maxValue = max;
                healthSlider.value = current;
            }

            if (healthText != null)
            {
                healthText.text = current.ToString();
            }
        }
    }
}
