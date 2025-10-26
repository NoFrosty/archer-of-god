using UnityEngine;

namespace ArcherOfGod.Core.AI.Actions
{
    public class AttackAction : IAIAction
    {
        private bool executed;

        public bool IsComplete { get; private set; }

        public AttackAction()
        {
            executed = false;
            IsComplete = false;
        }

        public void Execute(EnemyAIController controller)
        {
            if (executed) return;

            executed = true;
            IsComplete = true;
        }

        public void Reset()
        {
            executed = false;
            IsComplete = false;
        }
    }
}
