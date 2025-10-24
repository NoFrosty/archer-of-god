using ArcherOfGod.Core;
using UnityEngine;

namespace ArcherOfGod
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private PlayerController playerController;
        [SerializeField] private EnemyController enemyController;

        public PlayerController PlayerController => playerController;
        public EnemyController EnemyController => enemyController;

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
    }
}