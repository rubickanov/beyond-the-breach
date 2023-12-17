using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class SingleDoor : MonoBehaviour
    {
        [Header("Connect to button")]
        [SerializeField] BatterySocket[] floorButton;
        [SerializeField] float numberOfBtnsActivated;

        [SerializeField] Transform slidingDoor;
        [SerializeField] bool activated;
        [SerializeField] LayerMask layerToOpen;
        [SerializeField] float doorSpeed = 3f;
        [SerializeField] Transform detectionPos;
        [SerializeField] float boxDetectionSize = 3f;
        [SerializeField] Renderer[] doorRenderers;
        float doorInitPos;
        float targetXPos = 2.54f;
        float currentXPos;
        bool detected;

        public UnityEvent OnDoorActivated;
        public UnityEvent OnDoorOpening;
        public UnityEvent OnDoorClosing;

        bool doorActiveInvoke;
        bool doorOpeningInvoke;
        bool doorClosingInvoke;
        private void Start()
        {
            doorInitPos = slidingDoor.localPosition.x;

            SetMaterial(activated);
        }

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
                    button.onBatteryRemoved -= DeactivateDoor;
                }
            }
        }

        private void Update()
        {
            CheckForColliders();
        }

        private void CheckForColliders()
        {
            if (activated)
            {
                if (Physics.CheckBox(detectionPos.position, Vector3.one * boxDetectionSize, Quaternion.identity, layerToOpen))
                {
                    OpenDoor();
                    detected = true;
                }
                else
                {
                    detected = false;
                }
            }
            if (!activated || !detected)
            {
                CloseDoor();
            }
        }

        void OpenDoor()
        {
            if (slidingDoor.localPosition.x < targetXPos)
            {
                currentXPos = Mathf.Lerp(slidingDoor.localPosition.x, targetXPos, doorSpeed * Time.deltaTime);
                slidingDoor.localPosition = new Vector3(currentXPos, 0f, 0f);

                if (!doorOpeningInvoke)
                {
                    doorClosingInvoke = false;
                    doorOpeningInvoke = true;
                    OnDoorOpening.Invoke();
                }
            }
        }

        void CloseDoor()
        {
            if (slidingDoor.localPosition.x > doorInitPos)
            {
                currentXPos = Mathf.Lerp(slidingDoor.localPosition.x, doorInitPos, doorSpeed * Time.deltaTime);
                slidingDoor.localPosition = new Vector3(currentXPos, 0f, 0f);

                if (!doorClosingInvoke)
                {
                    doorOpeningInvoke = false;
                    doorClosingInvoke = true;
                    OnDoorClosing.Invoke();
                }
            }
        }

        public void SetDoor(bool enable)
        {
            activated = enable;
            SetMaterial(enable);

            if (enable)
            {
                SetSubtitle("[ Activated Door Sound ]", 3f);
                OnDoorActivated.Invoke();
            }
        }

        void ActivateDoor()
        {
            numberOfBtnsActivated++;
            if (numberOfBtnsActivated >= floorButton.Length)
            {
                activated = true;
                SetMaterial(true);

                if (!doorActiveInvoke)
                {
                    SetSubtitle("[ Activated Door Sound ]", 3f);

                    OnDoorActivated.Invoke();
                    doorActiveInvoke = true;
                }

            }
            else
            {
                SetMaterial(false);
                activated = false;
            }
        }
        void DeactivateDoor()
        {
            SetMaterial(false);
            doorActiveInvoke = false;
            numberOfBtnsActivated--;
            activated = false;
        }
        public void DeactivateDoorWithDelay()
        {
            StartCoroutine(DeactivateDoorDelay());
        }

        IEnumerator DeactivateDoorDelay()
        {
            yield return new WaitForSeconds(14);
            slidingDoor.localPosition = new Vector3(doorInitPos, 0f, 0f);
            SetMaterial(false);
            activated = false;
        }
        void SetMaterial(bool enable)
        {
            if (enable == true)
            {
                foreach (Renderer mat in doorRenderers)
                {
                    mat.material.EnableKeyword("_EMISSION");
                }
            }
            else
            {
                foreach (Renderer mat in doorRenderers)
                {
                    mat.material.DisableKeyword("_EMISSION");
                }
            }
        }

        void SetSubtitle(string txt, float subDuration)
        {
            if (SubtitleManager.Instance != null)
            {
                SubtitleManager.Instance.ShowSubtitle("", txt, subDuration);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(detectionPos.position, Vector3.one * boxDetectionSize);
        }
    }
}
