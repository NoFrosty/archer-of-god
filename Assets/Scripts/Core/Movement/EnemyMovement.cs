using UnityEngine;

namespace ArcherOfGod.Core.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour, IMovementController
    {
        [SerializeField] private float changeInterval = 2f;

        private Rigidbody2D rb;
        private CharacterAnimatorController animatorController;
        [SerializeField] private float moveSpeed = 5f;
        private float direction = 1f;
        private float changeDirectionTimer = 0f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animatorController = GetComponent<CharacterAnimatorController>();
        }

        public void Move(float deltaTime)
        {
            if (rb == null)
                return;

            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

            if (animatorController != null)
                animatorController.SetMoving(direction);

            changeDirectionTimer -= deltaTime;
            if (changeDirectionTimer <= 0f)
            {
                direction = Random.value < 0.5f ? -1f : 1f;
                changeDirectionTimer = changeInterval;
            }
        }

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }

        private void FixedUpdate()
        {
            Move(Time.fixedDeltaTime);
        }
    }
}
