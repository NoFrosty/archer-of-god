using ArcherOfGod.Core.Movement;
using UnityEngine;

namespace ArcherOfGod.Core
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyController : CharacterController
    {
        private IMovementController movementController;

        protected override void Awake()
        {
            base.Awake();
            movementController = GetComponent<IMovementController>();
        }
    }
}
