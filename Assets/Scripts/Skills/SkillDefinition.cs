using UnityEngine;

namespace ArcherOfGod.Skills
{
    [CreateAssetMenu(fileName = "NewSkill")]
    public class SkillDefinition : ScriptableObject
    {
        public SkillType skillType;
        public string skillName;
        [TextArea] public string description;
        public Sprite icon;
        public float cooldown = 5f;
    }
}
