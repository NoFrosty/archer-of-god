using UnityEngine;

namespace ArcherOfGod.Core
{
    public class ColdEffect : IStatusEffect
    {
        private Health target;
        private CharacterController characterController;
        private float duration;
        private float timer;
        private GameObject effectPrefab;
        private GameObject effectInstance;
        private float originalMoveSpeed;
        private bool speedReduced;

        public bool IsFinished { get; private set; }

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
            characterController = target.GetComponent<CharacterController>();
            if (characterController != null)
            {
                originalMoveSpeed = characterController.MoveSpeed;
                characterController.MoveSpeed = originalMoveSpeed * 0.5f;
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
            
            if (speedReduced && characterController != null)
            {
                characterController.MoveSpeed = originalMoveSpeed;
            }
            
            if (effectInstance != null)
            {
                Object.Destroy(effectInstance);
            }
        }
    }
}
