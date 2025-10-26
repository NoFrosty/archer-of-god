using UnityEngine;

namespace ArcherOfGod.Core
{
    public class BurnEffect : IStatusEffect
    {
        private Health target;
        private float duration;
        private float damagePerSecond;
        private float timer;
        private float tickTimer;
        private GameObject effectPrefab;
        private GameObject effectInstance;

        public bool IsFinished { get; private set; }

        public BurnEffect(float duration, float damagePerSecond, GameObject effectPrefab = null)
        {
            this.duration = duration;
            this.damagePerSecond = damagePerSecond;
            this.timer = 0f;
            this.tickTimer = 0f;
            this.effectPrefab = effectPrefab;
            this.IsFinished = false;
        }

        public void Apply(Health target)
        {
            this.target = target;
            if (effectPrefab != null && target != null)
            {
                // Instantiate and parent to target
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
            tickTimer += deltaTime;

            if (tickTimer >= 1f)
            {
                if (target.IsAlive)
                    target.TakeDamage(Mathf.RoundToInt(damagePerSecond));
                tickTimer = 0f;
            }

            if (timer >= duration)
            {
                Finish();
            }
        }

        private void Finish()
        {
            IsFinished = true;
            if (effectInstance != null)
            {
                Object.Destroy(effectInstance);
            }
        }
    }
}
