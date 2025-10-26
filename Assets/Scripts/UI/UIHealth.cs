using ArcherOfGod.Core;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArcherOfGod.UI
{
    /// <summary>
    /// Displays health bar, health text, and active status effect icons.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class UIHealth : MonoBehaviour
    {
        [Header("Health Display")]
        [SerializeField] private Health health;

        [Header("Status Effects")]
        [SerializeField] private StatusEffectIconsConfig statusEffectIcons;
        [SerializeField] private Transform statusEffectContainer;
        [SerializeField] private GameObject statusEffectIconPrefab;

        private Slider healthSlider;
        private TextMeshProUGUI healthText;
        private Dictionary<StatusEffectType, GameObject> activeEffectIcons = new Dictionary<StatusEffectType, GameObject>();

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

            if (statusEffectIcons == null)
            {
                Debug.LogWarning("StatusEffectIconsConfig is not assigned. Status effects will not be displayed.", this);
            }

            if (statusEffectContainer == null)
            {
                Debug.LogWarning("Status effect container is not assigned. Status effects will not be displayed.", this);
            }

            if (statusEffectIconPrefab == null)
            {
                Debug.LogWarning("Status effect icon prefab is not assigned. Status effects will not be displayed.", this);
            }

            health.HealthChanged += UpdateHealthUI;
            health.StatusEffectsChanged += UpdateStatusEffects;

            UpdateHealthUI(health.CurrentHealth, health.MaxHealth);
            UpdateStatusEffects(health.GetActiveEffectTypes());
        }

        private void OnDestroy()
        {
            if (health != null)
            {
                health.HealthChanged -= UpdateHealthUI;
                health.StatusEffectsChanged -= UpdateStatusEffects;
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

        private void UpdateStatusEffects(List<StatusEffectType> activeEffects)
        {
            if (statusEffectIcons == null || statusEffectIconPrefab == null || statusEffectContainer == null)
                return;

            var effectsToRemove = new List<StatusEffectType>();
            foreach (var kvp in activeEffectIcons)
            {
                if (!activeEffects.Contains(kvp.Key))
                {
                    effectsToRemove.Add(kvp.Key);
                }
            }

            foreach (var effectType in effectsToRemove)
            {
                if (activeEffectIcons.TryGetValue(effectType, out GameObject icon))
                {
                    Destroy(icon);
                    activeEffectIcons.Remove(effectType);
                }
            }

            foreach (var effectType in activeEffects)
            {
                if (!activeEffectIcons.ContainsKey(effectType))
                {
                    CreateStatusEffectIcon(effectType);
                }
            }
        }

        private void CreateStatusEffectIcon(StatusEffectType effectType)
        {
            Sprite iconSprite = statusEffectIcons.GetIcon(effectType);
            if (iconSprite == null)
            {
                Debug.LogWarning($"No icon found for status effect: {effectType}");
                return;
            }

            GameObject iconObj = Instantiate(statusEffectIconPrefab, statusEffectContainer);
            Image iconImage = iconObj.GetComponent<Image>();

            iconImage.sprite = iconSprite;
            iconImage.preserveAspect = true;

            RectTransform rect = iconObj.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(100, 100);

            activeEffectIcons[effectType] = iconObj;
        }
    }
}

