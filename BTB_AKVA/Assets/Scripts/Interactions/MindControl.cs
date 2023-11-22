using System.Collections;
using UnityEngine;
using AKVA.Player;
using System.Linq.Expressions;

namespace AKVA.Interaction
{
    public class MindControl : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;
        
        [SerializeField] private float distanceToMindControl;

        private MindControlledObject mindControlledObject;

        private bool isControlling;
        private Picking picking;
        [SerializeField] private GameObject playerVisual;
        private Mesh playerMesh;
        private Material playerMaterial;

        [HideInInspector] public bool IsActive;

        private void Awake()
        {
            picking = GetComponent<Picking>();
            playerMesh = playerVisual.GetComponent<MeshFilter>().mesh;
            playerMaterial = playerVisual.GetComponent<MeshRenderer>().material;
        }

        private void Start()
        {
            isControlling = false;
        }

        private void Update()
        {
            RaycastHit hit;
            if(!isControlling)
            {
                if(Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, distanceToMindControl))
                {
                    if(hit.transform.TryGetComponent(out mindControlledObject))
                    {
                        IsActive = true;
                        if(Input.GetKeyDown(PlayerInput.Instance.Controls.mindControl))
                        {
                            picking.DropObject();
                            Control(mindControlledObject);
                            mindControlledObject = hit.transform.GetComponent<MindControlledObject>();
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
            } else
            {
                IsActive = false;
                if(Input.GetKeyDown(PlayerInput.Instance.Controls.mindControl))
                {
                    picking.DropObject();
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
            StartCoroutine(SwapCoroutine(objectTransform));
        }

        private IEnumerator SwapCoroutine(Transform objectTransform) //FIX ROTATIONS!
        {
            PlayerInput.Instance.DisablePlayerMovement();
            yield return new WaitForFixedUpdate();
            Collider objectColl = objectTransform.GetComponent<Collider>();
            objectColl.enabled = false;
            
            Vector3 position = objectTransform.position;
            Vector3 forward = objectTransform.forward;

            objectTransform.position = transform.position;
            objectTransform.forward = transform.forward;

            transform.position = position;
            transform.forward = forward;
            
            yield return new WaitForFixedUpdate();
            objectColl.enabled = true;
            PlayerInput.Instance.EnablePlayerMovement();
        }

        private void OnDrawGizmosSelected()
        {
            Debug.DrawRay(playerCamera.position, playerCamera.forward * distanceToMindControl, Color.red);
        }
    }
}
