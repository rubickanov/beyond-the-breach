using UnityEngine;

namespace AKVA.Interaction
{
    public class PickUpDrop : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform pickUpTarget;
        [SerializeField] private float pickUpDistance;

        [SerializeField] private ObjectGrabbable objectGrabbable;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (objectGrabbable == null)
                {
                    if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, pickUpDistance))
                    {
                        if (hit.transform.TryGetComponent(out objectGrabbable))
                        {
                            objectGrabbable.Grab(pickUpTarget);
                        }
                    }
                }
                else
                {
                    objectGrabbable.Drop();
                    objectGrabbable = null;
                }
                 
            }
        }
    }
}
