using UnityEngine;
using AKVA.Player;

namespace AKVA.Interaction
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;
        
        [SerializeField] private float distanceToInteract;
        
        private IInteractable currentInteraction;

        public bool IsActive;

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, distanceToInteract))
            {
                if (hit.transform.TryGetComponent(out currentInteraction))
                {
                    IsActive = true;
                    if (Input.GetKeyDown(PlayerInput.Instance.Controls.interact))
                    {
                        currentInteraction.Interact();
                    }
                }
                else
                {
                    IsActive = false;
                }
            }
            else
            {
                IsActive = false;
            }
        }


        private void OnDrawGizmosSelected()
        {
            Debug.DrawRay(playerCamera.position, playerCamera.forward * distanceToInteract, Color.green);
        }
    }
}
