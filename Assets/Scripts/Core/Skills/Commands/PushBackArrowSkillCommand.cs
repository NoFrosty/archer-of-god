using UnityEngine;

namespace ArcherOfGod.Core.Skills.Commands
{
    public class PushBackArrowSkillCommand : ISkillCommand
    {
        private readonly SkillDefinition skillDefinition;

        public PushBackArrowSkillCommand(SkillDefinition definition)
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
