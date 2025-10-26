using ArcherOfGod.Core.AI.Actions;

namespace ArcherOfGod.Core.AI.States
{
    public class AttackState : IAIState
    {
        private WaitAction waitAction;

        public void Enter(EnemyAIController controller)
        {
            controller.SetMovementEnabled(false);
            
            float waitDuration = controller.AIConfig.attackStateDuration;
            waitAction = new WaitAction(waitDuration);
        }

        public void Update(EnemyAIController controller, float deltaTime)
        {
            if (waitAction == null) return;

            waitAction.Execute(controller);

            if (waitAction.IsComplete)
            {
                controller.TransitionToNextState();
            }
        }

        public void Exit(EnemyAIController controller)
        {
        }
    }
}
