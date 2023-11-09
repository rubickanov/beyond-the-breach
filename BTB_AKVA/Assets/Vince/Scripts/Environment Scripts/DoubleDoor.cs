using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class DoubleDoor : MonoBehaviour
    {
        [Header("Connect to button")]
        [SerializeField] BatterySocket[] floorButton;
        [SerializeField] float numberOfBtnsActivated;

        [Header("Door Settings")]
        [SerializeField] Transform leftDoor, rightDoor;
        [SerializeField] bool activated;
        [SerializeField] float doorSpeed = 3f;
        [SerializeField] Material doorActivated, doorDeactivated;

        [Header("RayCast")]
        [SerializeField] Transform rayOrigin;
        [SerializeField] LayerMask allowedToOpen;
        [SerializeField] float rayLength = 1f;
        RaycastHit hit;
        public float leftDoorInitPos, rightDoorInitPos;

        #region Properties
        public bool EnableDoor { set => activated = value; }
        #endregion

        private void OnEnable()
        {
            if (floorButton.Length > 0 || floorButton != null)
            {
                foreach (BatterySocket button in floorButton)
                {
                    button.onBatteryPlaced += ActivateDoor;
                    button.onBatteryRemoved += DeactivateDoor;
                }
            }
        }

        private void OnDisable()
        {
            if (floorButton.Length > 0)
            {
                foreach (BatterySocket button in floorButton)
                {
                    button.onBatteryPlaced -= ActivateDoor;
                    button.onBatteryRemoved -= ActivateDoor;
                }
            }
        }

        void Awake()
        {
            leftDoorInitPos = leftDoor.localPosition.x;
            rightDoorInitPos = rightDoor.localPosition.x;
        }

        void Update()
        {
            CheckPlayer();
        }

        private void CheckPlayer()
        {
            if (activated)
            {
                ChangeDoorColor(doorActivated);
                Ray ray = new Ray(rayOrigin.position, -rayOrigin.forward);
                if (Physics.Raycast(ray, out hit, rayLength, allowedToOpen))
                {
                    OpenDoor();
                }
                else
                {
                    CloseDoor();
                }
            }
            else
            {
                ChangeDoorColor(doorDeactivated);
            }

            if (numberOfBtnsActivated < 0)
            {
                numberOfBtnsActivated = 0;
            }
        }

        void ChangeDoorColor(Material material)
        {
            leftDoor.GetComponent<Renderer>().material = material;
            rightDoor.GetComponent<Renderer>().material = material;
        }

        void ActivateDoor()
        {
            numberOfBtnsActivated++;
            if (numberOfBtnsActivated >= floorButton.Length)
            {
                activated = true;
            }
            else
            {
                activated = false;
            }
        }

        void DeactivateDoor()
        {
            numberOfBtnsActivated--;
            activated = false;
        }

        private void OpenDoor()
        {
            float leftDoorTargetPos = -3.272f;
            float rightDoorTargetPos = 3.28f;

            if (leftDoor.localPosition.x > leftDoorTargetPos)
            {
                float newX = Mathf.Lerp(leftDoor.localPosition.x, leftDoorTargetPos, doorSpeed * Time.deltaTime);
                leftDoor.localPosition = new Vector3(newX, leftDoor.localPosition.y, leftDoor.localPosition.z);
            }

            if (rightDoor.localPosition.x < rightDoorTargetPos)
            {
                float newX = Mathf.Lerp(rightDoor.localPosition.x, rightDoorTargetPos, doorSpeed * Time.deltaTime);
                rightDoor.localPosition = new Vector3(newX, rightDoor.localPosition.y, rightDoor.localPosition.z);
            }
        }

        void CloseDoor()
        {
            float leftDoorTargetPos = leftDoorInitPos;
            float rightDoorTargetPos = rightDoorInitPos;

            if (leftDoor.localPosition.x < leftDoorTargetPos)
            {
                float newX = Mathf.Lerp(leftDoor.localPosition.x, leftDoorTargetPos, doorSpeed * Time.deltaTime);
                leftDoor.localPosition = new Vector3(newX, leftDoor.localPosition.y, leftDoor.localPosition.z);
            }

            if (rightDoor.localPosition.x > rightDoorTargetPos)
            {
                float newX = Mathf.Lerp(rightDoor.localPosition.x, rightDoorTargetPos, doorSpeed * Time.deltaTime);
                rightDoor.localPosition = new Vector3(newX, rightDoor.localPosition.y, rightDoor.localPosition.z);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + -rayOrigin.forward * rayLength);
        }
    }
}