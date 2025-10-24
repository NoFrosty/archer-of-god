using UnityEngine;
using UnityEngine.InputSystem;

namespace ArcherOfGod.Core
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private Vector2 moveValue;
        public float MoveInput => moveValue.x;

        public void OnMove(InputValue input)
        {
            moveValue = input.Get<Vector2>();
        }
    }
}
