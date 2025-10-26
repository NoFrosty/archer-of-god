using ArcherOfGod.Core.AI.Actions;
using UnityEngine;

namespace ArcherOfGod.Core.AI.States
{
    public class SkillState : IAIState
    {
        private UseSkillAction skillAction;
        private WaitAction waitAction;
        private bool skillUsed;

        public void Enter(EnemyAIController controller)
        {
            controller.SetMovementEnabled(false);
            skillUsed = false;

            int skillIndex = controller.GetRandomAvailableSkill();
            if (skillIndex >= 0)
            {
                skillAction = new UseSkillAction(skillIndex);
            }

            float waitDuration = controller.AIConfig.skillStateDuration;
            waitAction = new WaitAction(waitDuration);
        }

        public void Update(EnemyAIController controller, float deltaTime)
        {
            if (!skillUsed && skillAction != null)
            {
                skillAction.Execute(controller);
                skillUsed = true;
            }

            if (waitAction == null) return;

            waitAction.Execute(controller);

            if (waitAction.IsComplete)
            {
                controller.TransitionToNextState();
            }
        }

        public void Exit(EnemyAIController controller)
        {
        }
    }
}
