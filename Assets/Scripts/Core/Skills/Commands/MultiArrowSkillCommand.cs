using UnityEngine;

namespace ArcherOfGod.Core.Skills.Commands
{
    public class MultiArrowSkillCommand : ISkillCommand
    {
        private readonly SkillDefinition skillDefinition;

        public MultiArrowSkillCommand(SkillDefinition definition)
        {
            skillDefinition = definition;
        }

        public void Execute(CharacterController character)
        {
            var arrowShooter = character.GetComponent<ArrowShooter>();
            if (arrowShooter != null && skillDefinition != null)
            {
                arrowShooter.ShootMultiArrow(skillDefinition, 10);
            }
        }
    }
}
