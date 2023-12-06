using System;
using AKVA.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace AKVA.GameplayUI
{
    /// <summary>
    /// THIS FREAKING CODE SHOULD BE IN THE GOD DAMN GAMEPLAY UI BUT THANKS TO FUCKING USELESS ASSEMBLY DEFINITIONS I CANT PUT IT THERE AND NOT GET CYCLIC DEPENDENCY
    /// HELL NO I'LL USE THIS IDIOTIC PIECE OF SHIT ONCE AGAIN
    /// </summary>
    public class MindControlSlider : MonoBehaviour
    {
        [SerializeField] private Image fillInImage;
        [SerializeField] private MindControl mindControl;

        private float timerToMindControl;

        private void Update()
        {
            if (mindControl.GetTimeToMindControlValue() <= 0) return;
            timerToMindControl = mindControl.GetTimerToMindControlValue();
            
            if (timerToMindControl <= 0)
            {
                fillInImage.enabled = false;
            }
            else
            {
                fillInImage.enabled = true;
                fillInImage.fillAmount = timerToMindControl / mindControl.GetTimeToMindControlValue();
            }
        }
    }
}

