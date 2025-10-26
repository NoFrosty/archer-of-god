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
            Debug.Log($"{character.name} used Shield skill.");
        }
    }
}
