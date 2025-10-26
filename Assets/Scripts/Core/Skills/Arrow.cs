using System.Collections;
using UnityEngine;

namespace ArcherOfGod.Core
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
        [SerializeField] private Vector2 pushBackForce = new Vector2(5f, 2f);

        private ObjectPool pool;
        private float lifetimeTimer;
        private float originalGravityScale;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            originalGravityScale = rb.gravityScale;
        }

        private void OnEnable()
        {
            lifetimeTimer = 0f;
            rb.gravityScale = originalGravityScale;
        }

        private void Update()
        {
            lifetimeTimer += Time.deltaTime;
            if (lifetimeTimer >= lifetime)
            {
                ReturnToPool();
                return;
            }

            Vector2 v = rb.linearVelocity;
            if (v.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            }
        }

        public void Init(Vector3 target, Faction owner, SkillDefinition skillDefinition, ObjectPool objectPool)
        {
            enabled = true;
            rb.simulated = true;
            this.skillDefinition = skillDefinition;
            ownerType = owner;
            pool = objectPool;
            rb.gravityScale = originalGravityScale;

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

        public void InitDirect(Vector3 target, Faction owner, SkillDefinition skillDefinition, ObjectPool objectPool)
        {
            enabled = true;
            rb.simulated = true;
            this.skillDefinition = skillDefinition;
            ownerType = owner;
            pool = objectPool;

            rb.gravityScale = 0f;

            Vector2 direction = Vector2.right * Mathf.Sign(target.x - transform.position.x);
            float speed = skillDefinition != null ? skillDefinition.directShootSpeed : 15f;
            rb.linearVelocity = direction * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground"))
            {
                StartCoroutine(DelayedReturnToPool(2f));
                return;
            }

            var characterFaction = other.GetComponent<CharacterFaction>();
            if (characterFaction != null && characterFaction.IsAlly(ownerType))
                return;

            var health = other.GetComponent<Health>();
            if (health != null && health.IsAlive)
            {
                int finalDamage = skillDefinition != null ? skillDefinition.damage : damage;
                health.TakeDamage(finalDamage);

                ApplyStatusEffect(health, other.transform);
                transform.SetParent(other.transform);
                StartCoroutine(DelayedReturnToPool(2f));
            }
        }

        public IEnumerator DelayedReturnToPool(float delay)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;

            enabled = false;
            yield return new WaitForSeconds(delay);
            ReturnToPool();
        }

        private void ApplyStatusEffect(Health health, Transform target)
        {
            if (skillDefinition == null || health == null)
                return;

            switch (skillDefinition.effectType)
            {
                case ArrowEffectType.Fire:
                    var burn = new BurnEffect(burnDuration, burnDamagePerSecond, skillDefinition.effectPrefab);
                    health.AddEffect(burn);
                    break;
                case ArrowEffectType.Ice:
                    var cold = new ColdEffect(burnDuration, skillDefinition.effectPrefab);
                    health.AddEffect(cold);
                    break;
                case ArrowEffectType.PushBack:
                    Vector2 direction = (target.position - transform.position).normalized;
                    float horizontalSign = Mathf.Sign(direction.x);
                    Vector2 force = new Vector2(horizontalSign * pushBackForce.x, pushBackForce.y);
                    var pushBack = new PushBackEffect(0.3f, force);
                    health.AddEffect(pushBack);
                    break;
            }
        }

        private void ReturnToPool()
        {
            rb.gravityScale = originalGravityScale;
            transform.SetParent(null);

            if (pool != null)
            {
                pool.Return(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
