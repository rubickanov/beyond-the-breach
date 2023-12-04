using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class ActivateDoubleDoorForAI : MonoBehaviour
    {
        DoubleDoor door;
        [SerializeField] float timeToActivate = 3f;
        private void Start()
        {
            door = GetComponent<DoubleDoor>();
        }
        private void OnTriggerEnter(Collider other)
        {
           if(other.tag == "Scientist")
            {
                StartCoroutine(ActivateDoor());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Scientist")
            {
                if(door != null)
                {
                    StartCoroutine(DeactivateDoor());
                }
            }
        }
        IEnumerator DeactivateDoor()
        {
            yield return new WaitForSeconds(1);
            door.EnableDoor = false;
        }

        IEnumerator ActivateDoor()
        {
            yield return new WaitForSeconds(timeToActivate);
            door.EnableDoor = true;
        }
    }
}
