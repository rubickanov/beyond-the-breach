using System;
using AKVA.Assets.Vince.Scripts.Environment;
using UnityEngine;

namespace AKVA.Interaction
{
    public class LastCheckpointDoorHotfix : MonoBehaviour
    {
        private MindControl mindControl;
        private Switch switchScript;
        [SerializeField] private SingleDoor door;


        private void Start()
        {
            switchScript = GetComponentInParent<Switch>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (other.TryGetComponent(out mindControl))
                {
                    if (mindControl.isControlling)
                    {
                        switchScript.OnSwitchUp.AddListener(OpenDoor);
                        switchScript.OnSwitchDown.AddListener(CloseDoor); 
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                if (other.TryGetComponent(out mindControl))
                {
                    if (mindControl.isControlling)
                    {
                        //switchScript.OnSwitchUp.RemoveListener(CloseDoor);
                    }
                }
            }
        }

        private void CloseDoor()
        {
            door.enabled = false;
        }

        private void OpenDoor()
        {
            door.enabled = true;
        }
    }
}
