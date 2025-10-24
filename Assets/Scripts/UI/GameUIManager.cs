using ArcherOfGod.Core;
using UnityEngine;

namespace ArcherOfGod.UI
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject victoryScreen;
        [SerializeField] private GameObject defeatScreen;

        private void Awake()
        {
            if (victoryScreen != null) victoryScreen.SetActive(false);
            if (defeatScreen != null) defeatScreen.SetActive(false);
        }

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnVictory += ShowVictory;
                GameManager.Instance.OnDefeat += ShowDefeat;
                GameManager.Instance.OnRestart += HidePanels;
            }
        }
        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnVictory -= ShowVictory;
                GameManager.Instance.OnDefeat -= ShowDefeat;
            }
        }

        private void HidePanels()
        {
            if (victoryScreen != null) victoryScreen.SetActive(false);
            if (defeatScreen != null) defeatScreen.SetActive(false);
        }

        private void ShowVictory()
        {
            if (victoryScreen != null) victoryScreen.SetActive(true);
        }

        private void ShowDefeat()
        {
            if (defeatScreen != null) defeatScreen.SetActive(true);
        }
    }
}
