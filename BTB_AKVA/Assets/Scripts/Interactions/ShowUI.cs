using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AKVA.Interaction
{
    public class ShowUI : MonoBehaviour
    {
        [SerializeField] GameObject UI;

        private void Awake()
        {
            SetTheUI(false);
        }

        public void SetTheUI(bool value)
        {
            UI.SetActive(value);
        }

        public void SetInteractionText(string text)
        {
            UI.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
        }
    }
}
