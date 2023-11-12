using UnityEngine;

namespace AKVA.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSmoothTime;
        [SerializeField] private float gravity;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float walkSpeed;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius;

        private Rigidbody rb;
        private Vector3 playerInput;
        private Vector3 currentMoveVelocity;
        private Vector3 moveDampVelocity;

        private Vector3 currentForceVelocity;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
            //HandleJumpAndGravity();
        }

 

        private void HandleMovement()
        {
            playerInput = new Vector3
            {
                x = GetAxis(PlayerInput.Instance.Controls.left, PlayerInput.Instance.Controls.right),
                y = 0f,
                z = GetAxis(PlayerInput.Instance.Controls.backwards, PlayerInput.Instance.Controls.forward)
            };

            if (playerInput.magnitude > 1f)
            {
                playerInput.Normalize();
            }

            Vector3 moveVector = transform.TransformDirection(playerInput);
        
            // Check if shift (CAN BE ADDED HERE)

            currentMoveVelocity = Vector3.SmoothDamp(
                currentMoveVelocity, 
                moveVector * walkSpeed, 
                ref moveDampVelocity,
                moveSmoothTime
            );
            
            rb.velocity = new Vector3(currentMoveVelocity.x, currentForceVelocity.y, currentMoveVelocity.z);
        }

        private void HandleJumpAndGravity()
        {
            if (IsGrounded())
            {
                currentForceVelocity.y = -2f;
                if (Input.GetKeyDown(PlayerInput.Instance.Controls.jump))
                {
                    currentForceVelocity.y = jumpHeight;
                }
            }
            else
            {
                currentForceVelocity.y += gravity * Time.deltaTime;
            }

            //rb.velocity = new Vector3(rb.velocity.x, currentForceVelocity.y, rb.velocity.z);

        }
        
        private float GetAxis(KeyCode negative, KeyCode positive)
        {
            if (Input.GetKey(negative) && Input.GetKey(positive))
            {
                return 0;
            }
        
            if (Input.GetKey(negative))
            {
                return -1f;
            }

            if (Input.GetKey(positive))
            {
                return 1f;
            }

            return 0;
        }

        private bool IsGrounded()
        {
            return Physics.CheckSphere(groundCheck.position, groundCheckRadius);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
