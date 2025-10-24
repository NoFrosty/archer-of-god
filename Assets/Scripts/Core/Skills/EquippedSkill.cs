using System;

namespace ArcherOfGod.Core
{
    [Serializable]
    public class EquippedSkill
    {
        public SkillDefinition definition;
        public float cooldownTimer;

        public bool IsReady => cooldownTimer <= 0f;

        public void UpdateCooldown(float deltaTime)
        {
            if (cooldownTimer > 0f)
                cooldownTimer -= deltaTime;
        }

        public void Trigger()
        {
            cooldownTimer = definition.cooldown;
        }
    }
}
