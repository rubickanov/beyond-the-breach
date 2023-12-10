using System.Collections;
using UnityEngine;
using AKVA.Player;
using AKVA.Vince.SO;

namespace AKVA.Interaction
{
    public class MindControl : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;
        [SerializeField] private Transform orientation;

        [SerializeField] private float distanceToMindControl;

        private MindControlledObject mindControlledObject;

        bool isControlling;
        private Picking picking;
        [SerializeField] private GameObject playerVisual;
        private Mesh playerMesh;
        private Material playerMaterial;
        private SkinnedMeshRenderer skinnedMeshRenderer;

        [HideInInspector] public bool IsActive;
        [HideInInspector] public bool IsUnHacking;
        [SerializeField] private float timeToMindControl = 2f;
        [SerializeField] private bool hasMindControlLimit;
        [SerializeField] private float mindControlTimeLimit;

        private float mindControlTimer;
        private float timerToMindControl;

        [SerializeField] private bool canSwap = true;
        [SerializeField] BoolReference isMindControlling;


        private void Awake()
        {
            picking = GetComponent<Picking>();
            skinnedMeshRenderer = playerVisual.GetComponent<SkinnedMeshRenderer>();
            playerMesh = skinnedMeshRenderer.sharedMesh;
            playerMaterial = skinnedMeshRenderer.material;
        }

        private void Start()
        {
            isControlling = false;
        }

        private void Update()
        {
            RaycastHit hit;
            if (!isControlling)
            {
                if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, distanceToMindControl))
                {
                    if (hit.transform.TryGetComponent(out mindControlledObject))
                    {
                        IsActive = true;
                        if (Input.GetKey(PlayerInput.Instance.Controls.mindControl) && canSwap)
                        {
                            PlayerInput.Instance.DisablePlayerMouseInput();
                            playerCamera.transform.forward =
                                (mindControlledObject.transform.position - playerCamera.transform.position) + new Vector3(0, 1.5f, 0);
                            timerToMindControl += Time.deltaTime;
                            if (timerToMindControl >= timeToMindControl)
                            {
                                timerToMindControl = 0;
                                picking.DropObject();
                                Control(mindControlledObject);
                                mindControlledObject = hit.transform.GetComponent<MindControlledObject>();
                                PlayerInput.Instance.EnablePlayerMouseInput();
                            }
                        }
                    }
                    else
                    {
                        IsActive = false;
                        PlayerInput.Instance.EnablePlayerMouseInput();
                    }
                }
                else
                {
                    IsActive = false;
                    PlayerInput.Instance.EnablePlayerMouseInput();
                }
            }
            else
            {
                IsActive = false;
                if (Input.GetKey(PlayerInput.Instance.Controls.mindControl) && canSwap)
                {
                    IsUnHacking = true;
                    IsActive = true;

                    timerToMindControl += Time.deltaTime;
                    if (timerToMindControl >= timeToMindControl)
                    {
                        timerToMindControl = 0;
                        picking.DropObject();
                        ReturnToBody(mindControlledObject);
                    }
                }
            }

            if (Input.GetKeyUp(PlayerInput.Instance.Controls.mindControl))
            {
                timerToMindControl = 0;
                canSwap = true;
            }

            if (!IsActive)
            {
                timerToMindControl = 0;
            }

          

            if (hasMindControlLimit)
            {
                if (isControlling)
                {
                    mindControlTimer -= Time.deltaTime;
                    if (mindControlTimer <= 0)
                    {
                        picking.DropObject();
                        ReturnToBody(mindControlledObject);
                    }
                }
            }

            isMindControlling.value = isControlling;
        }

        public void Control(MindControlledObject controlledObject)
        {
            Swap(controlledObject.transform);
            controlledObject.TakePlayerAppearance(playerMesh, playerMaterial);
            isControlling = true;
            mindControlTimer = mindControlTimeLimit;
        }

        public void ReturnToBody(MindControlledObject controlledObject)
        {
            Swap(controlledObject.transform);
            controlledObject.ResetAppearance();
            isControlling = false;
            mindControlTimer = mindControlTimeLimit;
        }

        private void Swap(Transform objectTransform)
        {
            canSwap = false;
            StartCoroutine(SwapCoroutine(objectTransform));
        }

        private IEnumerator SwapCoroutine(Transform objectTransform)
        {
            PlayerInput.Instance.DisablePlayerInput();
            yield return new WaitForFixedUpdate();
            Collider objectColl = objectTransform.GetComponent<Collider>();
            objectColl.enabled = false;

            Vector3 position = objectTransform.position;
            float yRot = objectTransform.eulerAngles.y;

            objectTransform.position = transform.position;
            objectTransform.forward = transform.forward;

            transform.position = position;
            playerCamera.GetComponent<MouseLook>().SetYRotation(yRot);

            yield return new WaitForFixedUpdate();
            objectColl.enabled = true;
            PlayerInput.Instance.EnablePlayerInput();
        }

        public float GetTimerToMindControlValue()
        {
            return timerToMindControl;
        }

        public float GetTimeToMindControlValue()
        {
            return timeToMindControl;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(playerCamera.position, playerCamera.forward * distanceToMindControl, Color.red);
        }
    }
}