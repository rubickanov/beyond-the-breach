using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] Renderer [] doorRenderers;
        float doorInitPos;
        float targetXPos = 2.54f;
        float currentXPos;
        bool detected;
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
                    button.onBatteryRemoved -= ActivateDoor;
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
            }
        }

        void CloseDoor()
        {
            if (slidingDoor.localPosition.x > doorInitPos)
            {
                currentXPos = Mathf.Lerp(slidingDoor.localPosition.x, doorInitPos, doorSpeed * Time.deltaTime);
                slidingDoor.localPosition = new Vector3(currentXPos, 0f, 0f);
            }
        }

        public void SetDoor(bool enable)
        {
            activated = enable;
            SetMaterial(enable);
        }

        void ActivateDoor()
        {
            numberOfBtnsActivated++;
            if (numberOfBtnsActivated >= floorButton.Length)
            {
                activated = true;
                SetMaterial(true);
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
            slidingDoor.localPosition = new Vector3(doorInitPos, 0f,0f);
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


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(detectionPos.position, Vector3.one * boxDetectionSize);
        }
    }
}
