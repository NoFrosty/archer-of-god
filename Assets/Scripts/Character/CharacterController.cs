using ArcherOfGod.Core;
using ArcherOfGod.Skills;
using UnityEngine;

namespace ArcherOfGod.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CharacterAnimatorController))]
    [RequireComponent(typeof(ArrowShooter))]
    public class CharacterController : MonoBehaviour, IFactionProvider
    {
        protected Rigidbody2D rb;
        protected Health health;
        protected CharacterAnimatorController animatorController;
        protected ArrowShooter arrowShooter;


        [Header("Movement Settings")]
        [SerializeField] protected float moveSpeed = 5f;

        [Header("Character Settings")]
        [SerializeField] protected Faction faction;

        [Header("Skills")]
        [SerializeField] private EquippedSkill[] equippedSkills = new EquippedSkill[5];

        public Faction Faction => faction;

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
    }
}
