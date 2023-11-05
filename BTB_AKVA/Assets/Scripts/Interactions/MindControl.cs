using UnityEngine;
using AKVA.Player;

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

        [SerializeField] private Mesh playerMesh;
        [SerializeField] private Material playerMaterial;
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
                            Control(mindControlledObject);
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
                    ReturnToBody(mindControlledObject);
                }
            }
        }
        
        public void Control(MindControlledObject controlledObject)
        {
            Swap(controlledObject.transform);
            controlledObject.TakePlayerAppearance(playerMesh, playerMaterial);
            isControlling = true;
        }

        private void ReturnToBody(MindControlledObject controlledObject)
        {
            Swap(controlledObject.transform);
            controlledObject.ResetAppearance();
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
