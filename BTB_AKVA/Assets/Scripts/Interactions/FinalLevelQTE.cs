using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AKVA.Player;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

namespace AKVA.Interaction
{
    public class FinalLevelQTE : MonoBehaviour
    {
        [SerializeField] UnityEvent OnUpdateValue;

        private void OnEnable()
        {
            PlayerInput.Instance.DisablePlayerInput();
        }

        private void OnDisable()
        {
        }

        private void Update()
        {
            PlayerInput.Instance.DisablePlayerInput();
            UpdateSlider();
        }

        private void UpdateSlider()
        {
            if (Input.GetKeyDown(PlayerInput.Instance.Controls.interact))
            {
                OnUpdateValue.Invoke();
            }
        }
    }
}
