using UnityEngine;

namespace ArcherOfGod.Core.Skills.Commands
{
    public class ShieldSkillCommand : ISkillCommand
    {
        private readonly SkillDefinition skillDefinition;

        public ShieldSkillCommand(SkillDefinition definition)
        {
            skillDefinition = definition;
        }

        public void Execute(CharacterController character)
        {
            var shieldController = character.GetComponent<ShieldController>();
            if (shieldController == null)
            {
                shieldController = character.gameObject.AddComponent<ShieldController>();
            }

            if (shieldController != null && skillDefinition != null)
            {
                shieldController.ActivateShield(skillDefinition);
            }
        }
    }
}
