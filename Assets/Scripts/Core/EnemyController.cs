using ArcherOfGod.Core.Movement;
using UnityEngine;

namespace ArcherOfGod.Core
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyController : CharacterController
    {
        private EnemyMovement movementController;

        protected override void Awake()
        {
            base.Awake();
            movementController = GetComponent<EnemyMovement>();
            if (health != null)
                health.Died += OnDeath;
        }

        private void OnDeath()
        {
            if (movementController != null)
                movementController.enabled = false;
        }

        public override void Reset()
        {
            base.Reset();
            movementController.enabled = true;
        }

        private void OnDestroy()
        {
            if (health != null)
                health.Died -= OnDeath;
        }
    }
}
