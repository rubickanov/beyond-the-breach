using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Interaction
{
    public class LeverQTE : MonoBehaviour, IInteractable
    {
        [SerializeField] UnityEvent OnLeverPushed;
        [SerializeField] Transform leverTransform;
        [SerializeField] GameObject finalLevelQTE;
        [SerializeField] float rotationForce = 0.1f;
        bool disabled;
        void OnEnable()
        {
            leverTransform.Rotate(new Vector3(-99, 0f, 0f));
        }
        void Update()
        {
            if (leverTransform.localEulerAngles.x > 280f && leverTransform.localEulerAngles.x < 360f)
            {
                leverTransform.Rotate(new Vector3(-0.1f, 0f, 0f));
            }
        }

        public void ForceRotate()
        {
            OnLeverPushed.Invoke();
            leverTransform.Rotate(new Vector3(rotationForce, 0f, 0f));
        }
        
        public void Interact()
        {
            if (!disabled)
            {
                finalLevelQTE.SetActive(true);
            }
        }

        public void DisableLever()
        {
            disabled = true;
        }
    }
}
