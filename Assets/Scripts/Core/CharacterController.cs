using ArcherOfGod.Shared;
using UnityEngine;

namespace ArcherOfGod.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CharacterAnimatorController))]
    [RequireComponent(typeof(ArrowShooter))]
    [RequireComponent(typeof(CharacterFaction))]
    public class CharacterController : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Health health;
        protected CharacterAnimatorController animatorController;
        protected ArrowShooter arrowShooter;
        protected CharacterFaction characterFaction;
        protected Collider2D col;

        [Header("Movement Settings")]
        [SerializeField] protected float moveSpeed = 5f;

        [Header("Skills")]
        [SerializeField] private EquippedSkill[] equippedSkills = new EquippedSkill[5];

        public Faction Faction => characterFaction.Faction;
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public EquippedSkill[] EquippedSkills => equippedSkills;

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
            if (!TryGetComponent<ArrowShooter>(out arrowShooter))
            {
                Debug.LogError("CharacterController requires an ArrowShooter component.");
            }
            if (!TryGetComponent<CharacterFaction>(out characterFaction))
            {
                Debug.LogError("CharacterController requires a CharacterFaction component.");
            }
            col = GetComponent<Collider2D>();

            health.Died += OnDeath;
            health.HealthChanged += OnHealthChanged;
        }

        private void Update()
        {
            foreach (var skill in equippedSkills)
                skill?.UpdateCooldown(Time.deltaTime);
        }

        private void OnDeath()
        {
            Debug.Log("Character died: " + gameObject.name);

            // Disable everything
            enabled = false;
            rb.simulated = false;
            if (arrowShooter != null) arrowShooter.enabled = false;
            if (animatorController != null) animatorController.enabled = false;
            if (col != null) col.enabled = false;
            animatorController.SetMoving(0f);
            animatorController.PlayDeath();
        }

        private void OnHealthChanged(int current, int max)
        {
            if (current < max)
            {
                if (current > 0)
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

        public bool IsAlive()
        {
            return health.CurrentHealth > 0;
        }

        public void UseSkill(int index)
        {
            var skill = equippedSkills[index];
            if (skill == null || !skill.IsReady) return;

            switch (skill.definition.skillType)
            {
                case SkillType.SpecialArrow:
                    arrowShooter.ShootSpecialArrow(skill.definition);
                    break;
                case SkillType.Shield:
                    Debug.Log("Player used Shield skill.");
                    break;
            }

            skill.Trigger();
        }

        public void Reset()
        {
            // Restore health
            health.Heal(health.MaxHealth);

            // Re-enable everything
            enabled = true;
            rb.simulated = true;
            if (arrowShooter != null) arrowShooter.enabled = true;
            if (animatorController != null) animatorController.enabled = true;
            if (col != null) col.enabled = true;
        }
    }
}
