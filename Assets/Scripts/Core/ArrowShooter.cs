using ArcherOfGod.Shared;
using System.Linq;
using UnityEngine;

namespace ArcherOfGod.Core
{
    public class ArrowShooter : MonoBehaviour
    {
        [Header("Shooting Settings")]
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float shootInterval = 1.5f;
        [SerializeField] private float arrowSpeed = 10f;
        [SerializeField] private float arcHeight = 1.5f;

        private float shootTimer;
        private CharacterController characterController;
        private CharacterController cachedTarget;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
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
            if (arrowPrefab == null || shootPoint == null || characterController == null) return;

            if (cachedTarget == null || !cachedTarget.IsAlive())
            {
                Faction targetFaction = characterController.Faction == Faction.Player ? Faction.Enemy : Faction.Player;
                var targets = FindObjectsByType<CharacterController>(FindObjectsSortMode.None)
                    .Where(c => c.Faction == targetFaction && c.IsAlive())
                    .ToArray();

                if (targets.Length == 0) return;

                cachedTarget = targets
                    .OrderBy(t => Vector2.Distance(shootPoint.position, t.transform.position))
                    .First();
            }

            GameObject arrowObj = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
            var arrow = arrowObj.GetComponent<Arrow>();
            if (arrow != null)
            {

                arrow.Init(cachedTarget.transform.position,
                    characterController.Faction, null
                    );
            }

            characterController.Attack();
        }

        public void ShootSpecialArrow(SkillDefinition skillDefinition)
        {
            if (shootPoint == null || characterController == null) return;

            if (cachedTarget == null || !cachedTarget.IsAlive())
            {
                Faction targetFaction = characterController.Faction == Faction.Player ? Faction.Enemy : Faction.Player;
                var targets = FindObjectsByType<CharacterController>(FindObjectsSortMode.None)
                    .Where(c => c.Faction == targetFaction && c.IsAlive())
                    .ToArray();

                if (targets.Length == 0) return;

                cachedTarget = targets
                    .OrderBy(t => Vector2.Distance(shootPoint.position, t.transform.position))
                    .First();
            }

            GameObject arrowObj = Instantiate(skillDefinition.arrowPrefab, shootPoint.position, Quaternion.identity);
            var arrow = arrowObj.GetComponent<Arrow>();
            if (arrow != null)
            {

                arrow.Init(cachedTarget.transform.position,
                    characterController.Faction,
                    skillDefinition);
            }

            characterController.Attack();
        }
    }
}
