using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AKVA
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI fpsTitle;
        float fps;
        float updateTimer = .2f;


        private void Update()
        {
            UpdateFPSDisplay();
        }

        private void UpdateFPSDisplay()
        {
            updateTimer -= Time.deltaTime;
            if (updateTimer <= 0f)
            {
                fps = 1f / Time.unscaledDeltaTime;
                fpsTitle.text = $"FPS: {Mathf.Round(fps)}";
                updateTimer = .2f;
            }
        }
    }
}