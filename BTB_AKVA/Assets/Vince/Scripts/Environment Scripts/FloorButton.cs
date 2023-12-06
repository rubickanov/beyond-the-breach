using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class FloorButton : MonoBehaviour
    {
        [SerializeField] UnityEvent OnBtnPressed;
        [SerializeField] UnityEvent OnBtnReleased;
        [SerializeField] Transform button;
        [SerializeField] LayerMask layersToDetect;
        [SerializeField] Vector3 checkBoxSize;
        [SerializeField] Renderer btnMat;
        [SerializeField] bool active;
        Color initColor;
        Vector3 initButtonPos;

        private void Start()
        {
            initButtonPos = button.localPosition;
            initColor = btnMat.material.color;
            SetEmission(active);
        }

        private void Update()
        {
            CheckForColliders();
        }

        private void CheckForColliders()
        {
            if(Physics.CheckBox(button.position, checkBoxSize, Quaternion.identity, layersToDetect) && active){
                button.localPosition = new Vector3(0f, 0f, -0.00062f);
                SetMaterial(Color.blue);
                OnBtnPressed.Invoke();
            }
            else
            {
                OnBtnReleased.Invoke();
                button.localPosition = initButtonPos;
                SetMaterial(initColor);
            }
        }
        void SetEmission(bool enable)
        {
            if (enable)
            {
                btnMat.material.EnableKeyword("_EMISSION");
            }
            else
            {
                btnMat.material.DisableKeyword("_EMISSION");
            }
        }

        void SetMaterial(Color btnColor)
        {
            btnMat.material.color = btnColor;
        }

        public void SetButton(bool enable)
        {
            active = enable;
            SetEmission(enable);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(button.position, checkBoxSize);
        }

    }
}