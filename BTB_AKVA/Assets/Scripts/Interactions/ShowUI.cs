using System.Collections;
using System.Collections.Generic;
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
    }
}
