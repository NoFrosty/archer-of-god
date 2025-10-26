namespace ArcherOfGod.Core.AI
{
    public interface IAIState
    {
        void Enter(EnemyAIController controller);
        void Update(EnemyAIController controller, float deltaTime);
        void Exit(EnemyAIController controller);
    }
}
