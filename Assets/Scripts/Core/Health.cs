using System.Collections.Generic;
using UnityEngine;

namespace ArcherOfGod.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHP = 100;
        [SerializeField] private int currentHP;
        [SerializeField] private Transform fxSpawnPoint;

        public int MaxHealth => maxHP;
        public int CurrentHealth => currentHP;
        public bool IsAlive => currentHP > 0;
        public bool IsInvulnerable { get; private set; }

        public Transform FxSpawnPoint => fxSpawnPoint;

        public delegate void OnHealthChanged(int current, int max);
        public event OnHealthChanged HealthChanged;

        public delegate void OnDeath();
        public event OnDeath Died;

        private List<IStatusEffect> activeEffects = new List<IStatusEffect>();

        private void Awake()
        {
            currentHP = maxHP;
        }

        private void Update()
        {
            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                activeEffects[i].UpdateEffect(Time.deltaTime);
                if (activeEffects[i].IsFinished)
                    activeEffects.RemoveAt(i);
            }
        }

        public void TakeDamage(int dmg)
        {
            if (dmg <= 0 || !IsAlive || IsInvulnerable) return;

            currentHP = Mathf.Max(0, currentHP - dmg);
            Debug.Log($"{gameObject.name} took {dmg} dmg. HP={currentHP}/{maxHP}");

            HealthChanged?.Invoke(currentHP, maxHP);

            if (!IsAlive) Die();
        }

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

        public void AddEffect(IStatusEffect effect)
        {
            effect.Apply(this);
            activeEffects.Add(effect);
        }

        public void SetInvulnerable(bool invulnerable)
        {
            IsInvulnerable = invulnerable;
            Debug.Log($"{gameObject.name} invulnerability: {IsInvulnerable}");
        }
    }
}
