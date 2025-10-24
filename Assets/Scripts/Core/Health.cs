using UnityEngine;

namespace ArcherOfGod.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHP = 100;
        [SerializeField] private int currentHP;

        public int MaxHealth => maxHP;
        public int CurrentHealth => currentHP;

        public delegate void OnHealthChanged(int current, int max);
        public event OnHealthChanged HealthChanged;

        public delegate void OnDeath();
        public event OnDeath Died;

        private void Awake()
        {
            currentHP = maxHP;
        }

        public void TakeDamage(int dmg)
        {
            currentHP -= dmg;
            currentHP = Mathf.Max(0, currentHP);
            Debug.Log($"{gameObject.name} took {dmg} dmg. HP={currentHP}/{maxHP}");

            HealthChanged?.Invoke(currentHP, maxHP);

            if (currentHP <= 0) Die();
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
            gameObject.SetActive(false);
        }
    }
}
