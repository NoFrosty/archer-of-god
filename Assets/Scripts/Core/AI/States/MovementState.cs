using ArcherOfGod.Core.AI.Actions;
using ArcherOfGod.Core.Movement;

namespace ArcherOfGod.Core.AI.States
{
    public class MovementState : IAIState
    {
        private MoveAction moveAction;
        private IMovementController movementController;

        public void Enter(EnemyAIController controller)
        {
            movementController = controller.GetComponent<IMovementController>();
            
            float moveDuration = controller.AIConfig.moveDuration;
            moveAction = new MoveAction(moveDuration);
            
            controller.SetMovementEnabled(true);
        }

        public void Update(EnemyAIController controller, float deltaTime)
        {
            if (moveAction == null) return;

            moveAction.Execute(controller);

            if (moveAction.IsComplete)
            {
                controller.TransitionToNextState();
            }
        }

        public void Exit(EnemyAIController controller)
        {
            controller.SetMovementEnabled(false);
        }
    }
}
