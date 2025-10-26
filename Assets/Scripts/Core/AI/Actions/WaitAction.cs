using UnityEngine;

namespace ArcherOfGod.Core.AI.Actions
{
    public class WaitAction : IAIAction
    {
        private float duration;
        private float elapsedTime;

        public bool IsComplete { get; private set; }

        public WaitAction(float duration)
        {
            this.duration = duration;
            this.elapsedTime = 0f;
            this.IsComplete = false;
        }

        public void Execute(EnemyAIController controller)
        {
            if (IsComplete) return;

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= duration)
            {
                IsComplete = true;
            }
        }

        public void Reset()
        {
            elapsedTime = 0f;
            IsComplete = false;
        }
    }
}
