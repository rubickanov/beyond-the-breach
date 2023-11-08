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
        [SerializeField] float targetRot;
        float currentXRot;

        void Update()
        {
            RotateLever();
        }

        public void Activate()
        {
            if(!activate)
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
                        currentXRot = Mathf.Lerp(currentXRot, targetRot, 7f * Time.deltaTime);
                        OnleverUp.Invoke();
                    }
                    if (currentXRot > 88)
                    {
                        currentXRot = 90;
                        targetRot = -90;
                        activate = false;
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
    }
}
