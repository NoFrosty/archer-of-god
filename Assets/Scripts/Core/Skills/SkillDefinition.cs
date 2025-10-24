using UnityEngine;

namespace ArcherOfGod.Core
{
    [CreateAssetMenu(fileName = "NewSkill")]
    public class SkillDefinition : ScriptableObject
    {
        public SkillType skillType;
        public ArrowEffectType effectType;
        public string skillName;
        [TextArea] public string description;
        public Sprite icon;
        public float cooldown = 5f;
        public GameObject arrowPrefab;
        public GameObject effectPrefab;
    }
}
