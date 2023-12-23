using System;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class MovingPlatform : MonoBehaviour
    {
        public float platformSpeed = 3f;
        public Transform target;
        Vector3 initPos;
        public bool pos1, pos2;
        public UnityEvent OnMove;

        bool movementInvoked;
        private Rigidbody rb;

        private Transform playerTransform;
        private Collider col;

        [SerializeField] private GameObject underPlatformCollider;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            initPos = transform.position;
        }

        private void Start()
        {
            playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
            col = GetComponent<Collider>();
        }

        private void Update()
        {
            if (transform.position != initPos)
            {
                underPlatformCollider.SetActive(true);
            }
            else
            {
                underPlatformCollider.SetActive(false);
            }
        }

        void FixedUpdate()
        {
            MovePlatform();
        }

        private void MovePlatform()
        {
            if (pos1)
            {
                //transform.position = Vector3.MoveTowards(transform.position, initPos, platformSpeed * Time.deltaTime);
                rb.MovePosition(Vector3.MoveTowards(rb.position, initPos, platformSpeed * Time.fixedDeltaTime));

                if (!movementInvoked)
                {
                    movementInvoked = true;
                    OnMove.Invoke();
                }

                if (Vector3.Distance(transform.position, initPos) <= 0f)
                {
                    movementInvoked = false;
                    pos1 = false;
                }
            }
            else if (pos2)
            {
                //transform.position = Vector3.MoveTowards(transform.position, target.position, platformSpeed * Time.deltaTime);
                rb.MovePosition(Vector3.MoveTowards(rb.position, target.position, platformSpeed * Time.fixedDeltaTime));

                if (!movementInvoked)
                {
                    movementInvoked = true;
                    OnMove.Invoke();
                }

                if (Vector3.Distance(transform.position, target.position) <= 0f)
                {
                    movementInvoked = false;
                    pos2 = false;
                }
            }
        }

        public void GoToPos1()
        {
            pos1 = true;
        }

        public void GoToPos2()
        {
            pos2 = true;
        }

        private void OnDrawGizmos()
        {
            if (target == null) { return; }
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(target.position, .1f);
        }

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody objectRigidbody;
            if (other.TryGetComponent<Rigidbody>(out objectRigidbody))
            {
                other.transform.parent = transform;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            Rigidbody objectRigidbody;
            if (other.TryGetComponent<Rigidbody>(out objectRigidbody))
            {
                other.transform.parent = null;
            }
        }
    }
}
