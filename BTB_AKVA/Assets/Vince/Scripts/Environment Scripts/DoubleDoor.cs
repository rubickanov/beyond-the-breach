using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] bool keepItActive;
        [SerializeField] bool openDoor;
        [SerializeField] float doorSpeed = 3f;
        [SerializeField] float leftDoorTargetPos = -3.272f;
        [SerializeField] float rightDoorTargetPos = 3.28f;
        [SerializeField] Material doorActivated, doorDeactivated;

        [Header("RayCast")]
        [SerializeField] Transform rayOrigin;
        [SerializeField] LayerMask allowedToOpen;
        //[SerializeField] float rayLength = 1f;
        [SerializeField] Vector3 physicsBoxSize;
        RaycastHit hit;
        public float leftDoorInitPos, rightDoorInitPos;

        [Header("Events")]
        public UnityEvent OnDoorActive;
        public UnityEvent OnDoorOpened;
        public UnityEvent OnDoorClosing;
        bool doorOpeningInvoke;
        bool doorClosingInvoke;
        bool eventInvoked;

        #region Properties
        public bool EnableDoor { set => activated = value; get => activated; }
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
            if (openDoor)
            {
                OpenDoor();
            }

            if (!activated)
            {
                CloseDoor();
            }
        }

        private void CheckPlayer()
        {
            if (activated)
            {
                Ray ray = new Ray(rayOrigin.position, -rayOrigin.forward);
                if (Physics.CheckBox(rayOrigin.position, physicsBoxSize, Quaternion.identity,allowedToOpen))
                {
                    OpenDoor();
                }
                else
                {
                    doorOpeningInvoke = false;
                    CloseDoor();
                }
                ChangeDoorColor(doorActivated);

                if (!eventInvoked)
                {
                    SetSubtitle("[ Activated Door Sound ]", 3f);
                    eventInvoked = true;
                    OnDoorActive.Invoke();
                }
            }
            else
            {
                eventInvoked = false;
                ChangeDoorColor(doorDeactivated);
            }

            if (numberOfBtnsActivated < 0)
            {
                numberOfBtnsActivated = 0;
            }

            KeepDoorActive();
        }

        private void KeepDoorActive()
        {
            if(keepItActive)
            {
                activated = true;
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
            if (leftDoor.localPosition.x > leftDoorTargetPos)
            {
                float newX = Mathf.Lerp(leftDoor.localPosition.x, leftDoorTargetPos, doorSpeed * Time.deltaTime);
                leftDoor.localPosition = new Vector3(newX, leftDoor.localPosition.y, leftDoor.localPosition.z);

                if (!doorOpeningInvoke)
                {
                    doorOpeningInvoke = true;
                    OnDoorOpened.Invoke();
                }
            }
            else
            {
                doorOpeningInvoke = false;
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

                if (!doorClosingInvoke)
                {
                    doorClosingInvoke = true;
                    OnDoorClosing.Invoke();
                }

            }
            else
            {
                doorClosingInvoke = false;
            }

            if (rightDoor.localPosition.x > rightDoorTargetPos)
            {
                float newX = Mathf.Lerp(rightDoor.localPosition.x, rightDoorTargetPos, doorSpeed * Time.deltaTime);
                rightDoor.localPosition = new Vector3(newX, rightDoor.localPosition.y, rightDoor.localPosition.z);
            }
        }

        public void SetDoorToOpen(bool value)
        {
            openDoor = value;
        }

        void SetSubtitle(string txt, float subDuration)
        {
            if(SubtitleManager.Instance != null)
            {
                SubtitleManager.Instance.ShowSubtitle("", txt, subDuration);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + -rayOrigin.forward * rayLength);
            Gizmos.DrawWireCube(rayOrigin.position, physicsBoxSize);
        }
    }
}