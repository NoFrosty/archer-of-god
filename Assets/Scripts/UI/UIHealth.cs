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
            }

            if (healthText == null)
            {
                Debug.LogError("No TextMeshProUGUI component found in children of UIHealth.", this);
            }

            if (health != null)
            {
                health.HealthChanged += UpdateHealthUI;
                UpdateHealthUI(health.CurrentHealth, health.MaxHealth);
            }
        }

        private void UpdateHealthUI(int current, int max)
        {
            healthSlider.maxValue = max;
            healthSlider.value = current;
            healthText.text = current.ToString();
        }
    }
}
