using UnityEngine;

namespace ArcherOfGod.Core.AI.Actions
{
    public class UseSkillAction : IAIAction
    {
        private int skillIndex;
        private bool executed;

        public bool IsComplete { get; private set; }

        public UseSkillAction(int skillIndex)
        {
            this.skillIndex = skillIndex;
            executed = false;
            IsComplete = false;
        }

        public void Execute(EnemyAIController controller)
        {
            if (executed) return;

            var character = controller.GetComponent<CharacterController>();
            if (character != null)
            {
                character.UseSkill(skillIndex);
            }

            executed = true;
            IsComplete = true;
        }

        public void Reset()
        {
            executed = false;
            IsComplete = false;
        }
    }
}
