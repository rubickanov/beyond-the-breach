using AKVA.Controls;
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
        [SerializeField] private LayerMask groundMask;

        [SerializeField] private ControlsSO controls;
        
        private CharacterController controller;
        private Vector3 playerInput;
        private Vector3 currentMoveVelocity;
        private Vector3 moveDampVelocity;

        private Vector3 currentForceVelocity;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            playerInput = new Vector3
            {
                x = GetAxis(controls.left, controls.right),
                y = 0f,
                z = GetAxis(controls.backwards, controls.forward)
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

            controller.Move(currentMoveVelocity * Time.deltaTime);

            if (IsGrounded())
            {
                currentForceVelocity.y = -2f;
                if (Input.GetKey(controls.jump))
                {
                    currentForceVelocity.y = jumpHeight;
                }
            }
            else
            {
                currentForceVelocity.y -= gravity * Time.deltaTime;
            }

            controller.Move(currentForceVelocity * Time.deltaTime);
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
            return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        }

        public bool IsMoving()
        {
            return  playerInput.magnitude != 0 || currentForceVelocity.y >= 1f;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
