using ArcherOfGod.Character;
using UnityEngine;

namespace ArcherOfGod
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private PlayerController playerController;
        private EnemyController enemyController;

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