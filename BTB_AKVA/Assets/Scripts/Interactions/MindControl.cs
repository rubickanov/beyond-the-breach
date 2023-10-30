using UnityEngine;
using AKVA.Player;

namespace AKVA.Interaction
{
    public class MindControl : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;
        
        [SerializeField] private float distanceToMindControl;

        [SerializeField] private GameObject controlUI;

        private MindControlledObject mindControlledObject;
        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, distanceToMindControl))
            {
                if (hit.transform.TryGetComponent(out mindControlledObject))
                {
                    controlUI.SetActive(true);
                    if (Input.GetKeyDown(PlayerInput.Instance.Controls.interact))
                    {
                        Control(mindControlledObject.transform);
                    }
                }
            }
            else
            {
                controlUI.SetActive(false);
            }
        }
        
        public void Control(Transform objectTransform)
        {
            var position = transform.position;
            transform.position = objectTransform.position;
            objectTransform.position = position;
        }
        
        private void OnDrawGizmosSelected()
        {
            Debug.DrawRay(playerCamera.position, playerCamera.forward * distanceToMindControl, Color.red);
        }
    }
}
