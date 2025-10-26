using UnityEngine;

namespace ArcherOfGod.Core
{
    /// <summary>
    /// ScriptableObject that defines icons for different status effects.
    /// </summary>
    [CreateAssetMenu(fileName = "StatusEffectIcons", menuName = "Archer Of God/Status Effect Icons")]
    public class StatusEffectIconsConfig : ScriptableObject
    {
        [Header("Status Effect Icons")]
        [Tooltip("Icon displayed when burning")]
        public Sprite burnIcon;

        [Tooltip("Icon displayed when frozen/slowed")]
        public Sprite coldIcon;

        [Tooltip("Icon displayed when shielded")]
        public Sprite shieldIcon;

        [Tooltip("Icon displayed when pushed back")]
        public Sprite pushBackIcon;

        /// <summary>
        /// Gets the icon for a specific status effect type.
        /// </summary>
        public Sprite GetIcon(StatusEffectType effectType)
        {
            return effectType switch
            {
                StatusEffectType.Burn => burnIcon,
                StatusEffectType.Cold => coldIcon,
                StatusEffectType.Shield => shieldIcon,
                StatusEffectType.PushBack => pushBackIcon,
                _ => null
            };
        }
    }
}
