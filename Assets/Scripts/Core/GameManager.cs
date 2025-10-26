using System;
using UnityEngine;

namespace ArcherOfGod.Core
{
    /// <summary>
    /// Singleton manager that controls game state, handles win/lose conditions,
    /// and manages game restart functionality.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>Singleton instance of the GameManager.</summary>
        public static GameManager Instance { get; private set; }

        [SerializeField] private PlayerController playerController;
        [SerializeField] private EnemyController enemyController;

        /// <summary>Gets the player controller reference.</summary>
        public PlayerController PlayerController => playerController;

        /// <summary>Gets the enemy controller reference.</summary>
        public EnemyController EnemyController => enemyController;

        /// <summary>Invoked when the player defeats the enemy.</summary>
        public event Action OnVictory;

        /// <summary>Invoked when the player is defeated.</summary>
        public event Action OnDefeat;

        /// <summary>Invoked when the game restarts.</summary>
        public event Action OnRestart;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            if (playerController?.Health != null)
                playerController.Health.Died += HandlePlayerDeath;

            if (enemyController?.Health != null)
                enemyController.Health.Died += HandleEnemyDeath;
        }

        private void OnDestroy()
        {
            if (playerController?.Health != null)
                playerController.Health.Died -= HandlePlayerDeath;

            if (enemyController?.Health != null)
                enemyController.Health.Died -= HandleEnemyDeath;
        }

        private void HandlePlayerDeath()
        {
            Debug.Log("Player has been defeated.");
            OnDefeat?.Invoke();
        }

        private void HandleEnemyDeath()
        {
            Debug.Log("Enemy has been defeated.");
            OnVictory?.Invoke();
        }

        /// <summary>
        /// Restarts the game by resetting both player and enemy controllers.
        /// </summary>
        public void RestartGame()
        {
            OnRestart?.Invoke();

            if (playerController != null)
                playerController.Reset();

            if (enemyController != null)
                enemyController.Reset();
        }
    }
}
