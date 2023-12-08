using AKVA.Vince.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Interaction
{
    public class HandScannerDevice : MonoBehaviour, IInteractable
    {
        [SerializeField] BoolReference isMindControlling;
        [SerializeField] GameObject openDoorTxt, closeDoorTxt;
        [SerializeField] UnityEvent OnHandMatch;

        public void Interact()
        {
            ChangeDeviceText();
        }

        void ChangeDeviceText()
        {
            if (isMindControlling.value)
            {
                OnHandMatch.Invoke();
                openDoorTxt.SetActive(true);
                closeDoorTxt.SetActive(false);
            }
        }
    }
}
