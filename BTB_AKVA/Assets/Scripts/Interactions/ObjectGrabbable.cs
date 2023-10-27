using System.Timers;
using UnityEngine;

namespace AKVA.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class ObjectGrabbable : MonoBehaviour
    {
        private Rigidbody objectRigidbody;
        private Transform pickUpTarget;

        private void Awake()
        {
            objectRigidbody = GetComponent<Rigidbody>();
        }

        public void Grab(Transform pickUpTarget)
        {
            this.pickUpTarget = pickUpTarget;
            objectRigidbody.useGravity = false;
        }

        public void Drop()
        {
            this.pickUpTarget = null;
            objectRigidbody.useGravity = true;
        }

        private void FixedUpdate()
        {
            if (pickUpTarget != null)
            {
                float lerpSpeed = 10f;
                Vector3 targetPosition = pickUpTarget.position;

                // Smoothly move the object towards the target position
                objectRigidbody.MovePosition(Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed)); 
            }
            
            
        }
    }
}
