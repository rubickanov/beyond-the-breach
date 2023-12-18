using UnityEngine;

namespace AKVA.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator animator;
        private PlayerMovement movement;

        private const string IS_WALKING = "IsWalking";

        private void Start()
        {
            movement = GetComponentInParent<PlayerMovement>();
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            if (movement.IsWalking())
            {
                animator.SetBool(IS_WALKING, true);
            }
            else
            {
                animator.SetBool(IS_WALKING, false);
            }
        }
    }
}
