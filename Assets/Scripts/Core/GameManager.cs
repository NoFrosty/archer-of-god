using System;
using UnityEngine;

namespace ArcherOfGod.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private PlayerController playerController;
        [SerializeField] private EnemyController enemyController;

        public PlayerController PlayerController => playerController;
        public EnemyController EnemyController => enemyController;

        public event Action OnVictory;
        public event Action OnDefeat;
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