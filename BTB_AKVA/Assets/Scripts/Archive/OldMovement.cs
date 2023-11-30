using AKVA.Controls;
using UnityEngine;

namespace AKVA.Archive
{
    public class OldMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float gravity;
    
        private CharacterController controller;
        private Vector3 velocity;

        [SerializeField] private ControlsSO controls;

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float groundCheckRadius = 0.5f;

        [SerializeField] private float maxOxygen;
        private float oxygen;

        private bool isGrounded;
 
        private void Awake()
        {
            controller = GetComponent<CharacterController>();

            oxygen = maxOxygen;
        }

        private void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
        
            float x = GetAxis(controls.left, controls.right);
            float z = GetAxis(controls.backwards, controls.forward);
        
            Vector3 movementVector = (transform.right * x + transform.forward * z).normalized;

        

            controller.Move(movementVector * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            velocity.y += gravity * Time.deltaTime;
        
            controller.Move(velocity * Time.deltaTime);
        
            if (movementVector.magnitude != 0 || !isGrounded)
            {
                oxygen -= Time.deltaTime;
            }
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

        public float GetOxygen()
        {
            return oxygen;
        }
    }
}
