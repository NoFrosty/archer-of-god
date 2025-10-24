using UnityEngine;

namespace ArcherOfGod.Core
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimatorController : MonoBehaviour
    {
        protected Animator animator;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetMoving(float speed)
        {
            animator.SetBool("IsMoving", Mathf.Abs(speed) > 0.01f);
            animator.SetFloat("Speed", speed);
        }

        public void PlayAttack()
        {
            animator.SetTrigger("Attack");
        }

        public void PlayDeath()
        {
            animator.SetTrigger("Death");
        }

        public void PlayHit()
        {
            animator.SetTrigger("Hit");
        }
    }
}
