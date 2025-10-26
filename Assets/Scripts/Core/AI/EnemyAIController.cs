using ArcherOfGod.Core.AI.States;
using ArcherOfGod.Core.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace ArcherOfGod.Core.AI
{
    public class EnemyAIController : MonoBehaviour
    {
        [Header("AI Configuration")]
        [SerializeField] private AIConfig aiConfig;

        private IAIState currentState;
        private Dictionary<AIStateType, IAIState> states;
        private int currentStateIndex;
        private IMovementController movementController;
        private CharacterController characterController;
        private float lastSkillUseTime;
        private bool movementEnabled;

        public AIConfig AIConfig => aiConfig;

        private void Awake()
        {
            movementController = GetComponent<IMovementController>();
            characterController = GetComponent<CharacterController>();
            InitializeStates();
        }

        private void Start()
        {
            if (aiConfig == null)
            {
                Debug.LogError("AI Config is not assigned!", this);
                enabled = false;
                return;
            }

            if (aiConfig.stateSequence.Count == 0)
            {
                Debug.LogError("AI Config has no state sequence defined!", this);
                enabled = false;
                return;
            }

            currentStateIndex = 0;
            TransitionToState(aiConfig.stateSequence[currentStateIndex]);
        }

        private void InitializeStates()
        {
            states = new Dictionary<AIStateType, IAIState>
            {
                { AIStateType.Idle, new IdleState() },
                { AIStateType.Movement, new MovementState() },
                { AIStateType.Attack, new AttackState() },
                { AIStateType.Skill, new SkillState() }
            };
        }

        private void Update()
        {
            if (!characterController.IsAlive())
            {
                enabled = false;
                return;
            }

            currentState?.Update(this, Time.deltaTime);

            if (movementEnabled && movementController != null)
            {
                movementController.Move(Time.deltaTime);
            }
        }

        public void TransitionToNextState()
        {
            currentStateIndex = (currentStateIndex + 1) % aiConfig.stateSequence.Count;
            AIStateType nextStateType = aiConfig.stateSequence[currentStateIndex];
            TransitionToState(nextStateType);
        }

        private void TransitionToState(AIStateType stateType)
        {
            currentState?.Exit(this);

            if (states.TryGetValue(stateType, out IAIState newState))
            {
                currentState = newState;
                currentState.Enter(this);
                Debug.Log($"{gameObject.name} AI: Entering {stateType} state");
            }
            else
            {
                Debug.LogError($"State {stateType} not found in states dictionary!");
            }
        }

        public void SetMovementEnabled(bool enabled)
        {
            movementEnabled = enabled;
        }

        public int GetRandomAvailableSkill()
        {
            if (characterController == null) return -1;

            if (Time.time - lastSkillUseTime < aiConfig.skillCooldown)
                return -1;

            if (Random.value > aiConfig.skillUsageProbability)
                return -1;

            var skills = characterController.EquippedSkills;
            List<int> availableSkills = new List<int>();

            for (int i = 0; i < skills.Length; i++)
            {
                if (skills[i] != null && skills[i].IsReady && skills[i].definition != null)
                {
                    availableSkills.Add(i);
                }
            }

            if (availableSkills.Count == 0)
                return -1;

            lastSkillUseTime = Time.time;
            return availableSkills[Random.Range(0, availableSkills.Count)];
        }

        public void ForceTransitionTo(AIStateType stateType)
        {
            for (int i = 0; i < aiConfig.stateSequence.Count; i++)
            {
                if (aiConfig.stateSequence[i] == stateType)
                {
                    currentStateIndex = i;
                    TransitionToState(stateType);
                    return;
                }
            }

            Debug.LogWarning($"State {stateType} not found in state sequence!");
        }

        private void OnDrawGizmosSelected()
        {
            if (aiConfig == null || currentState == null) return;

            Gizmos.color = Color.yellow;
            Vector3 position = transform.position + Vector3.up * 2f;
            UnityEditor.Handles.Label(position, $"State: {GetCurrentStateName()}");
        }

        private string GetCurrentStateName()
        {
            if (currentStateIndex >= 0 && currentStateIndex < aiConfig.stateSequence.Count)
            {
                return aiConfig.stateSequence[currentStateIndex].ToString();
            }
            return "Unknown";
        }
    }
}
