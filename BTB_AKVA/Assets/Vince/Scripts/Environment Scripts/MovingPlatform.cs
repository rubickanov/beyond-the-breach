using System;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class MovingPlatform : MonoBehaviour
    {
        public float platformSpeed = 3f;
        public Transform target;
        Vector3 initPos;
        public bool pos1, pos2;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            initPos = transform.position;
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
                if (Vector3.Distance(transform.position, initPos) <= 0f)
                {
                    pos1 = false;
                }
            }
            else if (pos2)
            {
                //transform.position = Vector3.MoveTowards(transform.position, target.position, platformSpeed * Time.deltaTime);
                rb.MovePosition(Vector3.MoveTowards(rb.position, target.position, platformSpeed * Time.fixedDeltaTime));
                if (Vector3.Distance(transform.position, target.position) <= 0f)
                {
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
    }
}
