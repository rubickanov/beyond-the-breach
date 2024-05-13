using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AKVA.Interaction
{
    public class ShowUI : MonoBehaviour
    {
        [SerializeField] GameObject UI;
        [SerializeField] GameObject InteractableUI;
        [SerializeField] GameObject NoPowerUI;

        private void Awake()
        {
            SetTheUI(false);

            if (gameObject.tag == "Battery")
            {
                InteractableUI.SetActive(true);
                NoPowerUI.SetActive(false);
            }
        }

        public void SetTheUI(bool value)
        {
            UI.SetActive(value);
        }

        public void SetInteractionText(string text)
        {
            UI.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
        }

        public void SetActiveInteractableUI(bool active)
        {
            if (active)
            {
                InteractableUI.SetActive(true);
                NoPowerUI.SetActive(false);
            }
            else
            {
                InteractableUI.SetActive(false);
                NoPowerUI.SetActive(true);
            }
        }
    }
}
