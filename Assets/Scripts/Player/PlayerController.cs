using UnityEngine;

namespace ArcherOfGod.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private PlayerInputHandler inputHandler;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody2D>();
            inputHandler = GetComponent<PlayerInputHandler>();
        }

        private void Awake()
        {
            if (!TryGetComponent<Rigidbody2D>(out rb))
            {
                Debug.LogError("PlayerController requires a Rigidbody2D component.");
            }
            if (!TryGetComponent<PlayerInputHandler>(out inputHandler))
            {
                Debug.LogError("PlayerController requires a PlayerInputHandler component.");
            }
        }

        private void FixedUpdate()
        {
            float move = inputHandler.MoveInput;
            rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
        }
    }
}
