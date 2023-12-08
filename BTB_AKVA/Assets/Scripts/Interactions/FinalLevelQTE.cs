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
        [SerializeField] Slider slider;
        [SerializeField] float additionalValue = 5f;
        [SerializeField] float subtractingVal = 1f;
        [SerializeField] UnityEvent OnUpdateValue;

        private void OnEnable()
        {
            PlayerInput.Instance.DisablePlayerMovement();
            PlayerInput.Instance.DisablePlayerInput();
            slider.value = 0;
        }

        private void Update()
        {
            UpdateSlider();
        }

        private void UpdateSlider()
        {
            if (slider.value > 0)
            {
                slider.value -= subtractingVal * Time.deltaTime;
            }
            if (Input.GetKeyDown(PlayerInput.Instance.Controls.interact))
            {
                OnUpdateValue.Invoke();
                slider.value += additionalValue * Time.deltaTime;
            }
        }
    }
}
