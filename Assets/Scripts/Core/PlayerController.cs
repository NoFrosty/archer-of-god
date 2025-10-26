using ArcherOfGod.Core.Movement;
using UnityEngine;

namespace ArcherOfGod.Core
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : CharacterController
    {
        private IMovementController movementController;

        protected override void Awake()
        {
            base.Awake();

            movementController = GetComponent<IMovementController>();
            if (movementController == null)
            {
                Debug.LogError("PlayerController requires an IMovementController component.");
            }
        }

        private void FixedUpdate()
        {
            if (!IsAlive() || movementController == null)
                return;

            movementController.Move(Time.fixedDeltaTime);
        }
    }
}
