using UnityEngine;
using AKVA.Player;
using AKVA.Assets.Vince.Scripts.Environment;

namespace AKVA.Interaction
{
    public class Picking : MonoBehaviour
    {
        [SerializeField] private LayerMask PickupMask;
        [SerializeField] private Camera PlayerCam;
        [SerializeField] private Transform PickupTarget;
        [Space]
        [SerializeField] private float PickupRange;
        public Rigidbody CurrentObject;
        InteractableBattery battery;

        void Update()
        {
            if (Input.GetKeyDown(PlayerInput.Instance.Controls.pick))
            {
                if (CurrentObject)
                {
                    CurrentObject.useGravity = true;
                    CurrentObject = null;
                    return;
                }

                Ray CameraRay = PlayerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                if (Physics.Raycast(CameraRay, out RaycastHit HitInfo, PickupRange, PickupMask))
                {
                    CurrentObject = HitInfo.rigidbody;
                    CurrentObject.useGravity = false;
                }
            }
        }

        void FixedUpdate()
        {
            if (CurrentObject)
            {
                Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
                float DistanceToPoint = DirectionToPoint.magnitude;

                CurrentObject.transform.forward = transform.forward;
                CurrentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
            }

            BatteryInteraction();
        }

        void BatteryInteraction()
        {
            if (CurrentObject != null)
            {
                battery = CurrentObject.GetComponent<InteractableBattery>();

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
