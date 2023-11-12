using UnityEngine;
using AKVA.Player;
using AKVA.Assets.Vince.Scripts.Environment;
using UnityEngine.Serialization;

namespace AKVA.Interaction
{
    public class Picking : MonoBehaviour
    {
        [SerializeField] private LayerMask pickupMask;
        [SerializeField] private Transform playerCam;
        [SerializeField] private Transform pickupTarget;
        [SerializeField] private float pickupRange;

        private Rigidbody currentObject;
        InteractableBattery battery;

        public bool IsActive;

        private void Update()
        {
            if (currentObject == null)
            {
                if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit hit, pickupRange, pickupMask))
                {
                    IsActive = true;
                    if (Input.GetKeyDown(PlayerInput.Instance.Controls.pick))
                    {
                        PickObject(hit.rigidbody);
                    }
                }
                else
                {
                    IsActive = false;
                }    
            }
            else
            {
                IsActive = false;
                if (Input.GetKeyDown(PlayerInput.Instance.Controls.pick))
                {
                    DropObject();
                }
            }
            
        }

        void FixedUpdate()
        {
            if (currentObject)
            {
                Vector3 DirectionToPoint = pickupTarget.position - currentObject.position;
                float DistanceToPoint = DirectionToPoint.magnitude;

                currentObject.transform.forward = transform.forward;
                currentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
            }

            BatteryInteraction();
        }

        private void PickObject(Rigidbody pickedObject)
        {
            currentObject = pickedObject;
            currentObject.useGravity = false;
        }

        public void DropObject()
        {
            if (currentObject == null) return;
            
            currentObject.useGravity = true;
            currentObject = null;
        }

        void BatteryInteraction()
        {
            if (currentObject != null)
            {
                battery = currentObject.GetComponent<InteractableBattery>();

                if (battery != null)
                {
                    battery.batteryOnHand = true;
                }
            }
            else
            {
                if(battery != null)
                {
                    battery.batteryOnHand = false;
                    battery = null;
                }
            }
        }
    }
}
