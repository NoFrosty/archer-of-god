using UnityEngine;

namespace ArcherOfGod.Core.Movement
{
    public interface IMovementController
    {
        void Move(float deltaTime);
        void SetMoveSpeed(float speed);
        float GetMoveSpeed();
    }
}
