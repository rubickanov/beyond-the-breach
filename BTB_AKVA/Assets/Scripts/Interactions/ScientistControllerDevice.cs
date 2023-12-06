using AKVA.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Interaction
{
    public class ScientistControllerDevice : MonoBehaviour, IInteractable
    {
        [SerializeField] MindControl mindControl;
        [SerializeField] MindControlledObject mindControlledObject;
        bool mindControlling;

        void Update()
        {
            ReturnToBody();
        }

        private void ReturnToBody()
        {
            if (Input.GetKeyDown(PlayerInput.Instance.Controls.mindControl) && mindControlling)
            {
                mindControlling = false;
                mindControl.ReturnToBody(mindControlledObject);
            }
        }

        public void Control()
        {
            if (!mindControlling)
            {
                mindControlling = true;
            }
            mindControl.Control(mindControlledObject);
        }

        public void Interact()
        {
            Control();
        }
    }
}
