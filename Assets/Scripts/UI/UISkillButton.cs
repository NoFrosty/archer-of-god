using ArcherOfGod.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArcherOfGod.UI
{
    public class UISkillButton : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI cooldownText;
        [SerializeField] private Button button;

        private EquippedSkill skill;
        private int skillIndex;
        private System.Action<int> onClick;

        public void Init(EquippedSkill skill, int index, System.Action<int> onClickCallback)
        {
            this.skill = skill;
            this.skillIndex = index;
            this.onClick = onClickCallback;

            if (skill?.definition != null && iconImage != null)
            {
                iconImage.sprite = skill.definition.icon;
                iconImage.enabled = true;
            }
            else if (iconImage != null)
            {
                iconImage.enabled = false;
            }

            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => onClick?.Invoke(skillIndex));
            }
        }

        public void UpdateUI()
        {
            if (skill == null || skill.definition == null)
            {
                if (iconImage != null) iconImage.enabled = false;
                if (cooldownText != null) cooldownText.text = "";
                if (button != null) button.interactable = false;
                return;
            }

            if (iconImage != null)
            {
                iconImage.sprite = skill.definition.icon;
                iconImage.enabled = true;
            }

            if (skill.IsReady)
            {
                if (button != null) button.interactable = true;
                if (cooldownText != null) cooldownText.text = "";
            }
            else
            {
                if (button != null) button.interactable = false;
                if (cooldownText != null) cooldownText.text = Mathf.Ceil(skill.cooldownTimer).ToString();
            }
        }
    }
}
