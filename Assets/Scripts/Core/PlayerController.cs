using UnityEngine;

namespace ArcherOfGod.Core
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerController : CharacterController
    {
        private PlayerInputHandler inputHandler;

        protected override void Awake()
        {
            base.Awake();

            if (!TryGetComponent<PlayerInputHandler>(out inputHandler))
            {
                Debug.LogError("PlayerController requires a PlayerInputHandler component.");
            }
        }

        private void FixedUpdate()
        {
            if (!IsAlive() || inputHandler == null || rb == null)
                return;

            float move = inputHandler.MoveInput;
            rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

            if (animatorController != null)
                animatorController.SetMoving(move);
        }
    }
}
