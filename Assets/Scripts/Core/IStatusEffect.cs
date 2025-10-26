namespace ArcherOfGod.Core
{
    /// <summary>
    /// Interface for status effects that can be applied to Health components.
    /// </summary>
    public interface IStatusEffect
    {
        /// <summary>
        /// Applies the effect to the target.
        /// </summary>
        /// <param name="target">The health component to apply the effect to.</param>
        void Apply(Health target);

        /// <summary>
        /// Updates the effect each frame.
        /// </summary>
        /// <param name="deltaTime">Time since last update.</param>
        void UpdateEffect(float deltaTime);

        /// <summary>
        /// Returns true when the effect has expired.
        /// </summary>
        bool IsFinished { get; }

        /// <summary>
        /// Gets the type of this status effect.
        /// </summary>
        StatusEffectType EffectType { get; }
    }
}
