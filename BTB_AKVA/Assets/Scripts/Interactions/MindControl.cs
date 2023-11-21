using System;
using System.Collections;
using UnityEngine;
using AKVA.Player;
using UnityEngine.Serialization;

namespace AKVA.Interaction
{
    public class MindControl : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;
        
        [SerializeField] private float distanceToMindControl;

        private MindControlledObject mindControlledObject;

        private bool isControlling;
        private Picking picking;

        [SerializeField] private Mesh playerMesh;
        [SerializeField] private Material playerMaterial;

        public bool IsActive;

        private void Awake()
        {
            picking = GetComponent<Picking>();
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

        private IEnumerator SwapCoroutine(Transform objectTransform)
        {
            Collider objectColl = objectTransform.GetComponent<Collider>();
            objectColl.enabled = false;
            
            var position = objectTransform.position;
            Vector3 forward = objectTransform.forward;
            
            objectTransform.position = transform.position;
            objectTransform.forward = transform.forward; 
            
            transform.position = position;
            transform.forward = forward;
            
            yield return new WaitForSeconds(0.1f);
            objectColl.enabled = true;
        }

        private void OnDrawGizmosSelected()
        {
            Debug.DrawRay(playerCamera.position, playerCamera.forward * distanceToMindControl, Color.red);
        }
    }
}
