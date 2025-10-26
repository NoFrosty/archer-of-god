using UnityEngine;

namespace ArcherOfGod.Core.Skills.Commands
{
    public class SpecialArrowSkillCommand : ISkillCommand
    {
        private readonly SkillDefinition skillDefinition;

        public SpecialArrowSkillCommand(SkillDefinition definition)
        {
            skillDefinition = definition;
        }

        public void Execute(CharacterController character)
        {
            var arrowShooter = character.GetComponent<ArrowShooter>();
            if (arrowShooter != null && skillDefinition != null)
            {
                arrowShooter.ShootSpecialArrow(skillDefinition);
            }
        }
    }
}
