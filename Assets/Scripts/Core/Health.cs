using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcherOfGod.Core
{
    /// <summary>
    /// Manages health, damage, healing, and death for game entities.
    /// Supports status effects and invulnerability.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHP = 100;
        [SerializeField] private int currentHP;
        [SerializeField] private Transform fxSpawnPoint;

        /// <summary>Gets the maximum health value.</summary>
        public int MaxHealth => maxHP;

        /// <summary>Gets the current health value.</summary>
        public int CurrentHealth => currentHP;

        /// <summary>Returns true if health is above zero.</summary>
        public bool IsAlive => currentHP > 0;

        /// <summary>Returns true if entity is currently invulnerable to damage.</summary>
        public bool IsInvulnerable { get; private set; }

        /// <summary>Transform position where visual effects should spawn.</summary>
        public Transform FxSpawnPoint => fxSpawnPoint;

        /// <summary>Invoked when health changes. Parameters: (current, max)</summary>
        public delegate void OnHealthChanged(int current, int max);
        public event OnHealthChanged HealthChanged;

        /// <summary>Invoked when health reaches zero.</summary>
        public delegate void OnDeath();
        public event OnDeath Died;

        /// <summary>Invoked when a status effect is added or removed.</summary>
        public event Action<List<StatusEffectType>> StatusEffectsChanged;

        private List<IStatusEffect> activeEffects = new List<IStatusEffect>();

        private void Awake()
        {
            currentHP = maxHP;
        }

        private void Update()
        {
            bool effectsChanged = false;
            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                activeEffects[i].UpdateEffect(Time.deltaTime);
                if (activeEffects[i].IsFinished)
                {
                    activeEffects.RemoveAt(i);
                    effectsChanged = true;
                }
            }

            if (effectsChanged)
            {
                NotifyStatusEffectsChanged();
            }
        }

        /// <summary>
        /// Applies damage to this entity. Ignores if invulnerable or already dead.
        /// </summary>
        /// <param name="dmg">Amount of damage to apply.</param>
        public void TakeDamage(int dmg)
        {
            if (dmg <= 0 || !IsAlive || IsInvulnerable) return;

            currentHP = Mathf.Max(0, currentHP - dmg);
            Debug.Log($"{gameObject.name} took {dmg} dmg. HP={currentHP}/{maxHP}");

            HealthChanged?.Invoke(currentHP, maxHP);

            if (!IsAlive) Die();
        }

        /// <summary>
        /// Restores health, capped at maximum health.
        /// </summary>
        /// <param name="amount">Amount of health to restore.</param>
        public void Heal(int amount)
        {
            currentHP += amount;
            currentHP = Mathf.Min(currentHP, maxHP);
            Debug.Log($"{gameObject.name} healed {amount}. HP={currentHP}/{maxHP}");
            HealthChanged?.Invoke(currentHP, maxHP);
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name} died.");
            Died?.Invoke();
        }

        /// <summary>
        /// Adds a status effect to this entity.
        /// </summary>
        /// <param name="effect">The status effect to apply.</param>
        public void AddEffect(IStatusEffect effect)
        {
            effect.Apply(this);
            activeEffects.Add(effect);
            NotifyStatusEffectsChanged();
        }

        /// <summary>
        /// Sets the invulnerability state of this entity.
        /// </summary>
        /// <param name="invulnerable">True to make invulnerable, false to remove.</param>
        public void SetInvulnerable(bool invulnerable)
        {
            IsInvulnerable = invulnerable;
            Debug.Log($"{gameObject.name} invulnerability: {IsInvulnerable}");
            NotifyStatusEffectsChanged();
        }

        /// <summary>
        /// Gets a list of currently active status effect types.
        /// </summary>
        public List<StatusEffectType> GetActiveEffectTypes()
        {
            var effectTypes = new List<StatusEffectType>();

            foreach (var effect in activeEffects)
            {
                if (!effectTypes.Contains(effect.EffectType))
                {
                    effectTypes.Add(effect.EffectType);
                }
            }

            if (IsInvulnerable)
            {
                if (!effectTypes.Contains(StatusEffectType.Shield))
                {
                    effectTypes.Add(StatusEffectType.Shield);
                }
            }

            return effectTypes;
        }

        private void NotifyStatusEffectsChanged()
        {
            StatusEffectsChanged?.Invoke(GetActiveEffectTypes());
        }
    }
}
