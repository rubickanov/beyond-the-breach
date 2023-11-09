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
        float currentXRot;
        float targetRot = 90;
        bool interactionCooldown = false;

        void Update()
        {
            RotateLever();
        }

        public void Activate()
        {
            if (!activate && !interactionCooldown)
            {
                activate = true;
            }
        }

        private void RotateLever()
        {
            if (activate)
            {
                if (targetRot == 90)
                {
                    if (currentXRot < targetRot)
                    {
                        currentXRot = Mathf.Lerp(currentXRot, targetRot, 5f * Time.deltaTime);
                        OnleverUp.Invoke();
                    }
                    if (currentXRot > 88)
                    {
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
                        OnleverDown.Invoke();
                    }
                    if (currentXRot < -88)
                    {
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
