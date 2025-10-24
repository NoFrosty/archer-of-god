using ArcherOfGod.Core;
using UnityEngine;

namespace ArcherOfGod.Skills
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private int damage = 10;
        [SerializeField] private float lifetime = 3f;
        private Rigidbody2D rb;

        private Faction ownerType;

        [Header("Trajectory Settings")]
        [SerializeField] private float arcHeight = 1.5f;

        [Header("Arrow Effect")]
        private SkillDefinition skillDefinition;
        [SerializeField] private float burnDuration = 3f;
        [SerializeField] private float burnDamagePerSecond = 5f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, lifetime);
        }

        public void Init(Vector3 target, Faction owner, SkillDefinition skillDefinition)
        {
            this.skillDefinition = skillDefinition;
            ownerType = owner;

            Vector2 start = transform.position;
            Vector2 end = target;
            float gravity = Mathf.Abs(Physics2D.gravity.y);

            Vector2 displacement = end - start;
            float dx = displacement.x;
            float dy = displacement.y;

            float peakY = Mathf.Max(start.y, end.y) + arcHeight;

            float vy_peak = Mathf.Sqrt(2 * gravity * (peakY - start.y));
            float t_peak = vy_peak / gravity;

            float vy_end = Mathf.Sqrt(2 * gravity * (peakY - end.y));
            float t_end = vy_end / gravity;

            float totalTime = t_peak + t_end;
            if (totalTime <= 0.01f) totalTime = 0.5f;

            float vx = dx / totalTime;
            float vy = vy_peak;

            Vector2 velocity = new Vector2(vx, vy);
            if (float.IsNaN(velocity.x) || float.IsNaN(velocity.y))
                velocity = Vector2.right * 10f;

            rb.linearVelocity = velocity;
        }

        private void Update()
        {
            Vector2 v = rb.linearVelocity;
            if (v.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var factionProvider = other.GetComponent<IFactionProvider>();
            if (factionProvider != null && factionProvider.Faction == ownerType)
                return;

            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);

                if (skillDefinition != null && skillDefinition.effectType == ArrowEffectType.Fire)
                {
                    var burn = new BurnEffect(burnDuration, burnDamagePerSecond, skillDefinition?.effectPrefab);
                    health.AddEffect(burn);
                }

                Destroy(gameObject);
            }
        }
    }
}
