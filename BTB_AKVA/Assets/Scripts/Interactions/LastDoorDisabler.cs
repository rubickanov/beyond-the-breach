using System;
using AKVA.Assets.Vince.Scripts.Environment;
using UnityEngine;

namespace AKVA.Interaction
{
    public class LastDoorDisabler : MonoBehaviour
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
                        switchScript.OnSwitchUp.AddListener(EnableDoor);
                        switchScript.OnSwitchDown.AddListener(DisableDoor); 
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

        private void EnableDoor()
        {
            door.enabled = true;
        }

        private void DisableDoor()
        {
            door.enabled = false;
        }
    }
}
