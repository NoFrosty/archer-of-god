using ArcherOfGod.Core;
using UnityEngine;

namespace ArcherOfGod.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CharacterAnimatorController))]
    public class CharacterController : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Health health;
        protected CharacterAnimatorController animatorController;

        [Header("Movement Settings")]
        [SerializeField] protected float moveSpeed = 5f;

        protected virtual void Awake()
        {
            if (!TryGetComponent<Rigidbody2D>(out rb))
            {
                Debug.LogError("CharacterController requires a Rigidbody2D component.");
            }
            if (!TryGetComponent<CharacterAnimatorController>(out animatorController))
            {
                Debug.LogError("CharacterController requires a CharacterAnimatorController component.");
            }
            if (!TryGetComponent<Health>(out health))
            {
                Debug.LogError("CharacterController requires a Health component.");
            }

            health.Died += OnDeath;
            health.HealthChanged += OnHealthChanged;
        }

        private void OnDeath()
        {
            animatorController.PlayDeath();
        }

        private void OnHealthChanged(int current, int max)
        {
            if (current < max)
            {
                animatorController.PlayHit();
            }
        }

        public void Attack()
        {
            animatorController.PlayAttack();
        }

        private void OnDestroy()
        {
            health.Died -= OnDeath;
            health.HealthChanged -= OnHealthChanged;
        }
    }
}
