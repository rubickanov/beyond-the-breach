using UnityEngine;
using AKVA.Player;

namespace AKVA.Interaction
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] private Transform camera;
        
        [SerializeField] private float distanceToInteract;
        [SerializeField] private GameObject interactionUI;

        private IInteractable currentInteraction;

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.forward, out hit, distanceToInteract))
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
            Debug.DrawRay(camera.position, camera.forward * distanceToInteract, Color.green);
        }
    }
}
