using ArcherOfGod.Shared;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace ArcherOfGod.Core
{
    public class ArrowShooter : MonoBehaviour
    {
        [Header("Shooting Settings")]
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float shootInterval = 1.5f;
        [SerializeField] private float arrowSpeed = 10f;
        [SerializeField] private float arcHeight = 1.5f;

        private float shootTimer;
        private CharacterController characterController;
        private CharacterController cachedTarget;
        private ObjectPool arrowPool;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            arrowPool = GetComponent<ObjectPool>();
        }

        private void Update()
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Shoot(ArrowEffectType.None);
                shootTimer = 0f;
            }
        }

        public void Shoot(ArrowEffectType effectType)
        {
            if (arrowPool == null || shootPoint == null || characterController == null)
                return;

            var target = GetTarget();
            if (target == null)
                return;

            GameObject arrowObj = arrowPool.Get();
            arrowObj.transform.position = shootPoint.position;
            arrowObj.transform.rotation = Quaternion.identity;

            var arrow = arrowObj.GetComponent<Arrow>();
            if (arrow != null)
            {
                arrow.Init(target.transform.position, characterController.Faction, null, arrowPool);
            }

            characterController.Attack();
        }

        public void ShootSpecialArrow(SkillDefinition skillDefinition)
        {
            if (shootPoint == null || characterController == null || skillDefinition?.arrowPrefab == null)
                return;

            var target = GetTarget();
            if (target == null)
                return;

            GameObject arrowObj = Instantiate(skillDefinition.arrowPrefab, shootPoint.position, Quaternion.identity);
            var arrow = arrowObj.GetComponent<Arrow>();
            if (arrow != null)
            {
                arrow.Init(target.transform.position, characterController.Faction, skillDefinition, null);
            }

            characterController.Attack();
        }

        public void ShootSalvo(SkillDefinition skillDefinition, int arrowCount)
        {
            if (shootPoint == null || characterController == null || skillDefinition?.arrowPrefab == null)
                return;

            StartCoroutine(ShootSalvoCoroutine(skillDefinition, arrowCount));
        }

        private IEnumerator ShootSalvoCoroutine(SkillDefinition skillDefinition, int arrowCount)
        {
            for (int i = 0; i < arrowCount; i++)
            {
                var target = GetTarget();
                if (target == null)
                    yield break;

                GameObject arrowObj = Instantiate(skillDefinition.arrowPrefab, shootPoint.position, Quaternion.identity);
                var arrow = arrowObj.GetComponent<Arrow>();
                if (arrow != null)
                {
                    arrow.Init(target.transform.position, characterController.Faction, skillDefinition, null);
                }

                characterController.Attack();
                yield return new WaitForSeconds(0.15f);
            }
        }

        private CharacterController GetTarget()
        {
            if (cachedTarget != null && cachedTarget.IsAlive())
                return cachedTarget;

            Faction targetFaction = characterController.Faction == Faction.Player ? Faction.Enemy : Faction.Player;
            var targets = FindObjectsByType<CharacterController>(FindObjectsSortMode.None)
                .Where(c => c.Faction == targetFaction && c.IsAlive())
                .ToArray();

            if (targets.Length == 0)
                return null;

            cachedTarget = targets
                .OrderBy(t => Vector2.Distance(shootPoint.position, t.transform.position))
                .First();

            return cachedTarget;
        }
    }
}
