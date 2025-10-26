using UnityEngine;

namespace ArcherOfGod.Core.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, IMovementController
    {
        private Rigidbody2D rb;
        private PlayerInputHandler inputHandler;
        private CharacterAnimatorController animatorController;
        private float moveSpeed = 5f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            inputHandler = GetComponent<PlayerInputHandler>();
            animatorController = GetComponent<CharacterAnimatorController>();
        }

        public void Move(float deltaTime)
        {
            if (inputHandler == null || rb == null)
                return;

            float move = inputHandler.MoveInput;
            rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

            if (animatorController != null)
                animatorController.SetMoving(move);
        }

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }
    }
}
