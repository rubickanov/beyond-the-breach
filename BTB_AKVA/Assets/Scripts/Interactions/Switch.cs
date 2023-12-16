using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Interaction
{
    public class Switch : MonoBehaviour, IInteractable
    {
        [SerializeField] bool Interacted;
        [SerializeField] Transform armRoot;
        [SerializeField] UnityEvent OnSwitchUp;
        [SerializeField] UnityEvent OnSwitchDown;
        [SerializeReference] float interactionCooldownDuration = 1.0f;
        [SerializeField] Renderer[] deviceMat;
        public bool powerOn;
        float currentXRot;
        float targetRot = 36;
        bool interactionCooldown = false;
        bool eventInvoked;

        private void Awake()
        {
            EnableMatEmission(powerOn);
        }

        void Update()
        {
            RotateSwitch();
        }

        public void Activate()
        {
            if (!Interacted && !interactionCooldown && powerOn)
            {
                Interacted = true;
            }
        }

        private void RotateSwitch()
        {
            if (Interacted && powerOn)
            {
                if (targetRot == 36)
                {
                    if (currentXRot < targetRot)
                    {
                        currentXRot = Mathf.Lerp(currentXRot, targetRot, 5f * Time.deltaTime);
                        if (!eventInvoked)
                        {
                            eventInvoked = true;
                            OnSwitchDown.Invoke();
                        }
                    }
                    if (currentXRot > 35)
                    {
                        currentXRot = 36;
                        targetRot = -36;
                        eventInvoked = false;
                        Interacted = false;
                        StartInteractionCooldown();
                    }
                }
                else if (targetRot == -36)
                {
                    if (currentXRot > targetRot)
                    {
                        currentXRot = Mathf.Lerp(currentXRot, targetRot, 5f * Time.deltaTime);
                        if (!eventInvoked)
                        {
                            eventInvoked = true;
                            OnSwitchUp.Invoke();
                        }
                    }
                    if (currentXRot < -35)
                    {
                        currentXRot = -36;
                        targetRot = 36;
                        eventInvoked = false;
                        Interacted = false;
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

        public void EnableSwitch(bool enable)
        {
            powerOn = enable;
            EnableMatEmission(enable);
        }

        void EnableMatEmission(bool enable)
        {
            if (enable)
            {
                foreach(Renderer mat in deviceMat)
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