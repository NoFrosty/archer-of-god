using UnityEngine;

namespace ArcherOfGod.Core.Skills.Commands
{
    public class DirectShootSkillCommand : ISkillCommand
    {
        private readonly SkillDefinition skillDefinition;

        public DirectShootSkillCommand(SkillDefinition definition)
        {
            skillDefinition = definition;
        }

        public void Execute(CharacterController character)
        {
            var arrowShooter = character.GetComponent<ArrowShooter>();
            if (arrowShooter != null && skillDefinition != null)
            {
                arrowShooter.ShootDirectArrow(skillDefinition);
            }
        }
    }
}
