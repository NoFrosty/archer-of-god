namespace ArcherOfGod.Core.AI.Actions
{
    public interface IAIAction
    {
        void Execute(EnemyAIController controller);
        bool IsComplete { get; }
        void Reset();
    }
}
