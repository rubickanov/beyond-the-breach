using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class PasswordDevice : MonoBehaviour
    {
        [SerializeField] private float distanceToInteract;
        
        public string devicePassword;
        [SerializeField] UnityEvent OnPasswordMatch;
        [SerializeField] UnityEvent OnButtonPressed;
        [SerializeField] GameObject[] screen;
        [SerializeField] LayerMask passwordKeylayer;
        [SerializeField] Slot[] slots;
        [SerializeField] private Transform playerCamera;
        
        string currentPass = "";
        RaycastHit hit;
        int slotIndex;

        
        private void Awake()
        {
            playerCamera = Camera.main.transform;
        }

   

        void Update()
        {
            MouseInteraction();
        }

        private void MouseInteraction()
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward,out hit, distanceToInteract, passwordKeylayer))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform.name.Contains("Enter"))
                    {
                        TurnOnKeyLight(hit.transform);
                        if(currentPass == devicePassword)
                        {
                            PasswordMatch();
                        }
                        else
                        {
                            ResetPassword();
                        }
                    }
                    else if(currentPass.Length < 4)
                    {
                        OnButtonPressed.Invoke();
                        TurnOnKeyLight(hit.transform);
                        UpdateKeySlots(hit.transform);
                    }
                }
            }
        }

        void PasswordMatch()
        {
            screen[0].SetActive(false);
            screen[1].SetActive(true);
            OnPasswordMatch.Invoke();
        }
        
        void TurnOnKeyLight(Transform obj)
        {
            obj.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(DisableKeylight(obj.GetChild(0).gameObject));
        }

        void ResetPassword()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    slots[i].nKeys[j].SetActive(false);
                }
                slots[i].nKeys[slots[i].nKeys.Length - 1].SetActive(true);
            }
            slotIndex = 0;
            currentPass = "";
        }

        void UpdateKeySlots(Transform obj)
        {
            currentPass = $"{currentPass}{obj.name}";
            slots[slotIndex].nKeys[int.Parse(obj.name)].SetActive(true);
            slots[slotIndex].nKeys[slots[slotIndex].nKeys.Length - 1].SetActive(false);
            slotIndex++;
        }

        public void UnlockPassDevice()
        {
            PasswordMatch();
        }

        IEnumerator DisableKeylight(GameObject key)
        {
            yield return new WaitForSeconds(0.1f);
            key.gameObject.SetActive(false);
        }
    }
}
