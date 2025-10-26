using UnityEngine;

namespace ArcherOfGod.Core
{
    [CreateAssetMenu(fileName = "NewSkill", menuName = "Archer Of God/Skill Definition")]
    public class SkillDefinition : ScriptableObject
    {
        public SkillType skillType;
        public ArrowEffectType effectType;
        public string skillName;
        [TextArea] public string description;
        public Sprite icon;
        public float cooldown = 5f;
        public int damage = 10;
        public GameObject arrowPrefab;
        public GameObject effectPrefab;

        [Header("Shield Settings")]
        [Tooltip("Duration of shield in seconds")]
        public float shieldDuration = 3f;

        [Header("Direct Shoot Settings")]
        [Tooltip("Speed of direct shot arrows")]
        public float directShootSpeed = 15f;
    }
}
