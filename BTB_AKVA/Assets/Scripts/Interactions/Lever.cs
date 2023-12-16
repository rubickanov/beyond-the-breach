using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Interaction
{
    public class Lever : MonoBehaviour, IInteractable
    {
        [SerializeField] bool activate;
        [SerializeField] Transform armRoot;
        [SerializeField] UnityEvent OnleverUp;
        [SerializeField] UnityEvent OnleverDown;
        [SerializeReference] float interactionCooldownDuration = 1.0f;
        [SerializeField] Renderer[] deviceMat;
        float currentXRot;
        float targetRot = 90;
        bool interactionCooldown = false;
        public bool powerOn;

        bool leverInvoked;
        void Awake()
        {
            EnableMatEmission(powerOn);
        }

        void Update()
        {
            RotateLever();
        }

        public void Activate()
        {
            if (!activate && !interactionCooldown && powerOn)
            {
                activate = true;
            }
        }

        private void RotateLever()
        {
            if (activate && powerOn)
            {
                if (targetRot == 90)
                {
                    if (currentXRot < targetRot)
                    {
                        currentXRot = Mathf.Lerp(currentXRot, targetRot, 5f * Time.deltaTime);
                        if (!leverInvoked)
                        {
                            leverInvoked = true;
                            OnleverUp.Invoke();
                        }
                    }
                    if (currentXRot > 88)
                    {
                        leverInvoked = false;
                        currentXRot = 90;
                        targetRot = -90;
                        activate = false;
                        StartInteractionCooldown();
                    }
                }
                else if (targetRot == -90)
                {
                    if (currentXRot > targetRot)
                    {
                        currentXRot = Mathf.Lerp(currentXRot, targetRot, 5f * Time.deltaTime);
                        if (!leverInvoked)
                        {
                            leverInvoked = true;
                            OnleverDown.Invoke();
                        }
                    }
                    if (currentXRot < -88)
                    {
                        leverInvoked = false;
                        currentXRot = -90;
                        targetRot = 90;
                        activate = false;
                        StartInteractionCooldown();
                    }
                }
            }
            Quaternion newRotation = Quaternion.Euler(currentXRot, armRoot.localRotation.eulerAngles.y, armRoot.localRotation.eulerAngles.z);
            armRoot.localRotation = newRotation;
        }

        public void Interact()
        {
            Activate();
        }
        public void EnableLever(bool enable)
        {
            powerOn = enable;
            EnableMatEmission(enable);
        }

        void EnableMatEmission(bool enable)
        {
            if (enable)
            {
                foreach (Renderer mat in deviceMat)
                {
                    Material[] mats = mat.materials;
                    mats[mats.Length - 1].EnableKeyword("_EMISSION");
                }
            }
            else
            {
                foreach (Renderer mat in deviceMat)
                {
                    Material[] mats = mat.materials;
                    mats[mats.Length - 1].DisableKeyword("_EMISSION");
                }
            }
        }

        void StartInteractionCooldown()
        {
            interactionCooldown = true;
            StartCoroutine(EndInteractionCooldown());
        }

        IEnumerator EndInteractionCooldown()
        {
            yield return new WaitForSeconds(interactionCooldownDuration);
            interactionCooldown = false;
        }
    }
}
