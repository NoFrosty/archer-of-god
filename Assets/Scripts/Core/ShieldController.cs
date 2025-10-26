using UnityEngine;

namespace ArcherOfGod.Core
{
    public class ShieldController : MonoBehaviour
    {
        private bool isShieldActive;
        private float shieldDuration;
        private float shieldTimer;
        private GameObject shieldEffect;
        private CharacterController characterController;
        private Health health;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (isShieldActive)
            {
                shieldTimer += Time.deltaTime;
                if (shieldTimer >= shieldDuration)
                {
                    DeactivateShield();
                }
            }
        }

        public void ActivateShield(SkillDefinition skillDefinition)
        {
            if (isShieldActive)
            {
                DeactivateShield();
            }

            isShieldActive = true;
            shieldDuration = skillDefinition.shieldDuration;
            shieldTimer = 0f;

            if (health != null)
            {
                health.SetInvulnerable(true);
            }

            if (skillDefinition.effectPrefab != null)
            {
                shieldEffect = Instantiate(skillDefinition.effectPrefab, health.FxSpawnPoint);
                shieldEffect.transform.localPosition = Vector3.zero;
                shieldEffect.transform.localScale = Vector3.one * 2f;
            }

            Debug.Log($"{gameObject.name} activated shield for {shieldDuration} seconds!");
        }

        private void DeactivateShield()
        {
            isShieldActive = false;

            if (health != null)
            {
                health.SetInvulnerable(false);
            }

            if (shieldEffect != null)
            {
                Destroy(shieldEffect);
                shieldEffect = null;
            }

            Debug.Log($"{gameObject.name} shield deactivated!");
        }

        private void OnDestroy()
        {
            if (shieldEffect != null)
            {
                Destroy(shieldEffect);
            }
        }
    }
}
