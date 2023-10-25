using UnityEngine;
using AKVA.Player;

namespace AKVA.Interaction
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] private float distanceToInteract;
        [SerializeField] private GameObject interactionUI;

        [SerializeField] private Transform transformToAttach;

        private IInteractable currentInteraction;

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distanceToInteract))
            {
                if (hit.transform.TryGetComponent(out currentInteraction))
                {
                    interactionUI.SetActive(true);
                    if (Input.GetKeyDown(PlayerInput.Instance.Controls.interact))
                    {
                        currentInteraction.Interact();
                    }
                }
            }
            else
            {
                interactionUI.SetActive(false);
            }
        }


        private void OnDrawGizmosSelected()
        {
            Debug.DrawRay(transform.position, transform.forward*distanceToInteract, Color.green);
        }
    }
}
