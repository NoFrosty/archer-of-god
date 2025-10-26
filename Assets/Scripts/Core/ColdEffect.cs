using ArcherOfGod.Core.Movement;
using UnityEngine;

namespace ArcherOfGod.Core
{
    /// <summary>
    /// Status effect that slows movement speed and displays a visual effect.
    /// </summary>
    public class ColdEffect : IStatusEffect
    {
        private Health target;
        private IMovementController movementController;
        private float duration;
        private float timer;
        private GameObject effectPrefab;
        private GameObject effectInstance;
        private float originalMoveSpeed;
        private bool speedReduced;

        public bool IsFinished { get; private set; }
        public StatusEffectType EffectType => StatusEffectType.Cold;

        /// <summary>
        /// Creates a new cold effect.
        /// </summary>
        /// <param name="duration">How long the slow lasts in seconds.</param>
        /// <param name="effectPrefab">Optional visual effect prefab.</param>
        public ColdEffect(float duration, GameObject effectPrefab = null)
        {
            this.duration = duration;
            this.effectPrefab = effectPrefab;
            this.timer = 0f;
            this.IsFinished = false;
            this.speedReduced = false;
        }

        public void Apply(Health target)
        {
            this.target = target;
            movementController = target.GetComponent<IMovementController>();
            if (movementController != null)
            {
                originalMoveSpeed = movementController.GetMoveSpeed();
                movementController.SetMoveSpeed(originalMoveSpeed * 0.5f);
                speedReduced = true;
            }

            if (effectPrefab != null && target != null)
            {
                effectInstance = Object.Instantiate(effectPrefab, target.FxSpawnPoint);
                effectInstance.transform.localScale = Vector3.one * 2;
                effectInstance.transform.localPosition = Vector3.zero;
            }
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
            IsFinished = true;

            if (speedReduced && movementController != null)
            {
                movementController.SetMoveSpeed(originalMoveSpeed);
            }

            if (effectInstance != null)
            {
                Object.Destroy(effectInstance);
            }
        }
    }
}
