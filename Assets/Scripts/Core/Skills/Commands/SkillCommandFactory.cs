using UnityEngine;

namespace ArcherOfGod.Core.Skills.Commands
{
    public static class SkillCommandFactory
    {
        public static ISkillCommand CreateCommand(SkillDefinition definition)
        {
            if (definition == null)
                return null;

            return definition.skillType switch
            {
                SkillType.SpecialArrow => new SpecialArrowSkillCommand(definition),
                SkillType.Shield => new ShieldSkillCommand(definition),
                SkillType.TripleShot => new TripleShotSkillCommand(definition),
                SkillType.PushBackArrow => new PushBackArrowSkillCommand(definition),
                SkillType.MultiArrow => new MultiArrowSkillCommand(definition),
                SkillType.DirectShoot => new DirectShootSkillCommand(definition),
                _ => null
            };
        }
    }
}
