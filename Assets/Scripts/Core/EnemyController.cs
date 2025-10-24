using UnityEngine;

namespace ArcherOfGod.Core
{
    public class EnemyController : CharacterController
    {
        private float direction = 1f;
        private float changeDirectionTimer = 0f;
        private float changeInterval = 2f;

        private void FixedUpdate()
        {
            if (IsAlive())
            {
                rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
                animatorController.SetMoving(direction);

                changeDirectionTimer -= Time.fixedDeltaTime;
                if (changeDirectionTimer <= 0f)
                {
                    direction = Random.value < 0.5f ? -1f : 1f;
                    changeDirectionTimer = changeInterval;
                }
            }
        }
    }
}
