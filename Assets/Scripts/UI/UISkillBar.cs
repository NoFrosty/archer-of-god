using ArcherOfGod.Core;
using UnityEngine;

namespace ArcherOfGod.UI
{
    public class UISkillBar : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerController characterController;
        [SerializeField] private Transform skillButtonParent;
        [SerializeField] private GameObject skillButtonPrefab;

        private UISkillButton[] skillButtons;

        private void Awake()
        {
            if (characterController == null)
                characterController = FindAnyObjectByType<PlayerController>();

            int skillCount = characterController.EquippedSkills.Length;
            skillButtons = new UISkillButton[skillCount];

            for (int i = 0; i < skillCount; i++)
            {
                var skill = characterController.EquippedSkills[i];
                var btnObj = Instantiate(skillButtonPrefab, skillButtonParent);
                var uiSkillBtn = btnObj.GetComponent<UISkillButton>();
                skillButtons[i] = uiSkillBtn;
                uiSkillBtn.Init(skill, i, OnSkillButtonClicked);
            }
        }

        private void Update()
        {
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i]?.UpdateUI();
            }
        }

        private void OnSkillButtonClicked(int index)
        {
            characterController.UseSkill(index);
        }
    }
}
