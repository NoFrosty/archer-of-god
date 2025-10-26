using UnityEngine;

namespace ArcherOfGod.Core.AI.States
{
    public class IdleState : IAIState
    {
        private float waitTime;
        private float timer;

        public void Enter(EnemyAIController controller)
        {
            controller.SetMovementEnabled(false);
            waitTime = controller.AIConfig.idleDuration;
            timer = 0f;
        }

        public void Update(EnemyAIController controller, float deltaTime)
        {
            timer += deltaTime;

            if (timer >= waitTime)
            {
                controller.TransitionToNextState();
            }
        }

        public void Exit(EnemyAIController controller)
        {
        }
    }
}
