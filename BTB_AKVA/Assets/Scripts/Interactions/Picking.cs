using UnityEngine;
using AKVA.Player;
using AKVA.Assets.Vince.Scripts.Environment;

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

        [HideInInspector] public bool IsActive;

        ShowUI objUI;

        private void Update()
        {
            if (currentObject == null)
            {
                if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit hit, pickupRange, pickupMask))
                {
                    ShowUI(hit);
                    IsActive = true;
                    if (Input.GetKeyDown(PlayerInput.Instance.Controls.pick))
                    {
                        PickObject(hit.rigidbody);
                    }
                }
                else
                {
                    DisableUI();
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
                DisableUI();
            }

            BatteryInteraction();
        }

        private void PickObject(Rigidbody pickedObject)
        {
            pickedObject.interpolation = RigidbodyInterpolation.Interpolate;
            pickedObject.collisionDetectionMode = CollisionDetectionMode.Continuous;
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
                if (battery != null)
                {
                    battery.batteryOnHand = false;
                    battery = null;
                }
            }
        }

        void ShowUI(RaycastHit hit)
        {
            if (hit.transform.GetComponent<ShowUI>() != null && currentObject == null)
            {
                if (objUI == null)
                {
                    objUI = hit.transform.GetComponent<ShowUI>();
                    objUI.SetTheUI(true);
                }
            }
        }

        void DisableUI()
        {
            if (objUI != null)
            {
                objUI.SetTheUI(false);
                objUI = null;
            }
        }
    }
}
