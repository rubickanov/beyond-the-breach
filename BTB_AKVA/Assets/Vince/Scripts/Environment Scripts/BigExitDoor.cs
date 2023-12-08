using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class BigExitDoor : MonoBehaviour
    {

        [SerializeField] bool openGate;
        [SerializeField] float gateSpeed = 1f;
        [SerializeField] GameObject gate;
        [SerializeField] Renderer gateRenderer;
        [SerializeField] float targetYPos;
        [SerializeField] float forceOpenStrength;
        [SerializeField] UnityEvent ReachedDestination;
        [SerializeField] UnityEvent DoorFullyOpened;
        Vector3 initGatePos;
        float currentYPos;
        bool brokenDoor;
        bool doorFullyOpened;
        void Start()
        {
            initGatePos = gate.transform.localPosition;
            SetMaterialEmission(openGate);
        }

        void Update()
        {
            OpenDoor();
        }

        private void OpenDoor()
        {
            if (openGate && !brokenDoor && !doorFullyOpened)
            {
                SetMaterialEmission(true);
                if (gate.transform.localPosition.y > targetYPos)
                {
                    float currentYPos = Mathf.Lerp(gate.transform.localPosition.y, targetYPos, gateSpeed * Time.deltaTime);
                    gate.transform.localPosition = new Vector3(gate.transform.localPosition.x, currentYPos, gate.transform.localPosition.z);
                }
                if (gate.transform.localPosition.y <= -2.4f)
                {

                    Debug.Log("Reached gate destination");
                    ReachedDestination.Invoke();
                    brokenDoor = true;
                    gate.transform.localPosition = new Vector3(gate.transform.localPosition.x, targetYPos, gate.transform.localPosition.z);
                }
            }
            else
            {
                SetMaterialEmission(false);

                if (gate.transform.localPosition.y < initGatePos.y && !brokenDoor)
                {
                    float currentYPos = Mathf.Lerp(gate.transform.localPosition.y, initGatePos.y, gateSpeed * Time.deltaTime);
                    gate.transform.localPosition = new Vector3(gate.transform.localPosition.x, currentYPos, gate.transform.localPosition.z);
                }
            }

            if (brokenDoor && gate.transform.localPosition.y <= -6.66f)
            {
                doorFullyOpened = true;
                DoorFullyOpened.Invoke();
            }
            if (brokenDoor && gate.transform.localPosition.y < 0 && doorFullyOpened)
            {
                StartCoroutine(CloseDoor());
            }

        }

        IEnumerator CloseDoor()
        {
            yield return new WaitForSeconds(2.5f);
            float currentYPos = Mathf.Lerp(gate.transform.localPosition.y, 0f, .05f * Time.deltaTime);
            gate.transform.localPosition = new Vector3(gate.transform.localPosition.x, currentYPos, gate.transform.localPosition.z);
        }

        public void ForceOpenDoor()
        {
            gate.transform.localPosition += new Vector3(0f, forceOpenStrength * Time.deltaTime, 0f);
        }

        public void SetGate(bool value)
        {
            openGate = value;
        }

        void SetMaterialEmission(bool value)
        {
            if (value)
            {
                gateRenderer.material.EnableKeyword("_EMISSION");
            }
            else
            {
                gateRenderer.material.DisableKeyword("_EMISSION");
            }
        }
    }
}
