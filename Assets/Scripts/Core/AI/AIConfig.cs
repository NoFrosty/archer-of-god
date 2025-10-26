using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcherOfGod.Core.AI
{
    [Serializable]
    public enum AIStateType
    {
        Idle,
        Movement,
        Attack,
        Skill
    }

    [CreateAssetMenu(fileName = "NewAIConfig", menuName = "Archer Of God/AI Configuration")]
    public class AIConfig : ScriptableObject
    {
        [Header("State Durations")]
        [Tooltip("Duration of idle state in seconds")]
        public float idleDuration = 0.5f;

        [Tooltip("Duration of movement state in seconds")]
        public float moveDuration = 3f;

        [Tooltip("Duration to wait in attack state (for attack cooldown)")]
        public float attackStateDuration = 1f;

        [Tooltip("Duration to wait in skill state (for skill animation)")]
        public float skillStateDuration = 1.5f;

        [Header("Behavior Pattern")]
        [Tooltip("Define the sequence of states the AI will cycle through")]
        public List<AIStateType> stateSequence = new List<AIStateType>
        {
            AIStateType.Movement,
            AIStateType.Attack,
            AIStateType.Skill,
            AIStateType.Idle
        };

        [Header("Skill Usage")]
        [Tooltip("Probability (0-1) of using a skill when in Skill state")]
        [Range(0f, 1f)]
        public float skillUsageProbability = 0.7f;

        [Tooltip("Minimum cooldown between skill uses")]
        public float skillCooldown = 5f;
    }
}
