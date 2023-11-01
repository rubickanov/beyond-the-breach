using UnityEngine;
using AKVA.Player;
using System.Collections;
using System;

namespace AKVA.Interaction
{
    public class MindControl : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;
        
        [SerializeField] private float distanceToMindControl;

        [SerializeField] private GameObject controlUI;
        [SerializeField] private GameObject cancelControlUI;

        private MindControlledObject mindControlledObject;

        private bool isControlling = false;


        private void Update()
        {
            RaycastHit hit;
            if(!isControlling)
            {
                cancelControlUI.SetActive(false);
                if(Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, distanceToMindControl))
                {
                    if(hit.transform.TryGetComponent(out mindControlledObject))
                    {
                        controlUI.SetActive(true);
                        if(Input.GetKeyDown(PlayerInput.Instance.Controls.interact))
                        {
                            Control(mindControlledObject.transform);
                        }
                    }
                }
                else
                {
                    controlUI.SetActive(false);
                }
            } else
            {
                controlUI.SetActive(false);
                cancelControlUI.SetActive(true);
                if(Input.GetKeyDown(PlayerInput.Instance.Controls.interact))
                {
                    ReturnToBody(mindControlledObject.transform);
                }
            }
        }
        
        public void Control(Transform objectTransform)
        {
            Swap(objectTransform);
            isControlling = true;
        }

        private void ReturnToBody(Transform objectTransform)
        {
            Swap(objectTransform);
            isControlling = false;
        }

        private void Swap(Transform objectTransform)
        {
            var position = objectTransform.position;
            Vector3 forward = objectTransform.forward;
            CharacterController controller = GetComponent<CharacterController>();
            controller.enabled = false;
            objectTransform.position = transform.position;
            objectTransform.forward = transform.forward;
            transform.position = position;
            transform.forward = forward;
            controller.enabled = true;
        }

        private void OnDrawGizmosSelected()
        {
            Debug.DrawRay(playerCamera.position, playerCamera.forward * distanceToMindControl, Color.red);
        }
    }
}
