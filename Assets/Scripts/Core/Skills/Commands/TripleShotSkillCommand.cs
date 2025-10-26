using UnityEngine;

namespace ArcherOfGod.Core.Skills.Commands
{
    public class TripleShotSkillCommand : ISkillCommand
    {
        private readonly SkillDefinition skillDefinition;

        public TripleShotSkillCommand(SkillDefinition definition)
        {
            skillDefinition = definition;
        }

        public void Execute(CharacterController character)
        {
            var arrowShooter = character.GetComponent<ArrowShooter>();
            if (arrowShooter != null && skillDefinition != null)
            {
                arrowShooter.ShootSalvo(skillDefinition, 3);
            }
        }
    }
}
