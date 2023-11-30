using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class SmallGate : MonoBehaviour
    {
        [SerializeField] bool openGate;
        [SerializeField] Transform rightGate, leftGate;
        [SerializeField] float gateSpeed = 4f;
        float lerpTime;
        float targetRot = 90f;
        void Start()
        {

        }

        void Update()
        {
            OpenAndCloseGate();
        }

        private void OpenAndCloseGate()
        {
            lerpTime = gateSpeed * Time.deltaTime;

            if (openGate)
            {
                rightGate.rotation = Quaternion.Euler(0f, Mathf.LerpAngle(rightGate.localEulerAngles.y, targetRot, lerpTime), 0f);

                leftGate.rotation = Quaternion.Euler(0f, Mathf.LerpAngle(leftGate.localEulerAngles.y, -targetRot, lerpTime), 0f);
            }
            else
            {
                rightGate.rotation = Quaternion.Euler(0f, Mathf.LerpAngle(rightGate.localEulerAngles.y, 0, lerpTime), 0f);

                leftGate.rotation = Quaternion.Euler(0f, Mathf.LerpAngle(leftGate.localEulerAngles.y, 0, lerpTime), 0f);
            }
        }

        public void ToggleGate(bool enable)
        {
            openGate = enable;
        }
    }
}
