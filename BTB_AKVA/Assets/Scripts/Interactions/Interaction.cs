using UnityEngine;
using AKVA.Player;

namespace AKVA.Interaction
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;
        
        [SerializeField] private float distanceToInteract;
        
        private IInteractable currentInteraction;

        [HideInInspector] public bool IsActive;

        //UI
        ShowUI objUI;

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, distanceToInteract))
            {
                if (hit.transform.TryGetComponent(out currentInteraction))
                {
                    ShowUI(hit);
                    IsActive = true;
                    if (Input.GetKeyDown(PlayerInput.Instance.Controls.interact))
                    {
                        currentInteraction.Interact();
                    }
                }
                else
                {
                    DisableUI();
                    IsActive = false;
                }
            }
            else
            {
                DisableUI();
                IsActive = false;
            }
        }


        private void OnDrawGizmosSelected()
        {
            Debug.DrawRay(playerCamera.position, playerCamera.forward * distanceToInteract, Color.green);
        }

        void ShowUI(RaycastHit hit)
        {
            if (hit.transform.GetComponent<ShowUI>() != null)
            {
                if (objUI == null)
                {
                    objUI = hit.transform.GetComponent<ShowUI>();
                    objUI.SetTheUI(true);
                }
            }
        }
        void DisableUI()
        {
            if (objUI != null)
            {
                objUI.SetTheUI(false);
                objUI = null;
            }
        }
    }
}
