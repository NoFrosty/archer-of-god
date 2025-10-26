using ArcherOfGod.Core.Movement;
using UnityEngine;

namespace ArcherOfGod.Core
{
    /// <summary>
    /// Status effect that applies a knockback force to the target.
    /// </summary>
    public class PushBackEffect : IStatusEffect
    {
        private Health target;
        private IMovementController movementScript;
        private Rigidbody2D targetRb;
        private Vector2 pushForce;
        private float duration;
        private float timer;

        public bool IsFinished { get; private set; }
        public StatusEffectType EffectType => StatusEffectType.PushBack;

        /// <summary>
        /// Creates a new push back effect.
        /// </summary>
        /// <param name="duration">How long the effect lasts in seconds.</param>
        /// <param name="force">The force vector to apply.</param>
        public PushBackEffect(float duration, Vector2 force)
        {
            this.duration = duration;
            this.pushForce = force;
            this.timer = 0f;
            this.IsFinished = false;
        }

        public void Apply(Health target)
        {
            this.target = target;
            targetRb = target.GetComponent<Rigidbody2D>();
            movementScript = target.GetComponent<IMovementController>();

            if (movementScript is MonoBehaviour mb)
                mb.enabled = false;

            if (targetRb != null)
                targetRb.AddForce(pushForce, ForceMode2D.Impulse);
        }

        public void UpdateEffect(float deltaTime)
        {
            if (IsFinished || target == null)
                return;

            timer += deltaTime;

            if (timer >= duration)
            {
                Finish();
            }
        }

        private void Finish()
        {
            if (movementScript is MonoBehaviour mb)
                mb.enabled = true;

            IsFinished = true;
        }
    }
}

