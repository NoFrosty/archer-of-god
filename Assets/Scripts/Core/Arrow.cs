using UnityEngine;

namespace ArcherOfGod.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Arrow : MonoBehaviour
    {
        public int damage = 10;
        public float lifetime = 3f;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, lifetime);
        }

        public void Launch(Vector2 velocity)
        {
            if (velocity.sqrMagnitude > 0.001f)
                transform.right = velocity.normalized;

            rb.linearVelocity = velocity;

            Debug.Log($"Arrow launched vel={velocity}");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var hp = other.GetComponent<Health>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
