namespace ArcherOfGod.Core
{
    public interface IStatusEffect
    {
        void Apply(Health target);
        void UpdateEffect(float deltaTime);
        bool IsFinished { get; }
    }
}
