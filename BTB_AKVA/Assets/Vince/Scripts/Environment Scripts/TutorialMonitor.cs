using AKVA.Assets.Vince.Scripts.Environment;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class TutorialMonitor : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI keyLetters, instructionTxt, pressTxt;
        [SerializeField] InteractableBattery[] playerAssignedBattery;
        [SerializeField] Transform whiteBG;
        public bool turnOnTV;
        float whiteBgInitScale;
        bool liningUp;
        void Start()
        {
            InitWhiteScreen();
        }

        private void Update()
        {
            if (turnOnTV)
            {
                if (whiteBG.localScale.y < whiteBgInitScale)
                {
                    float currentScale = Mathf.Lerp(whiteBG.localScale.y, whiteBgInitScale, 6 * Time.deltaTime);
                    whiteBG.localScale = new Vector3(whiteBG.localScale.x, currentScale, whiteBG.localScale.z);
                }

                if (playerAssignedBattery.Length > 0 && !liningUp)
                {
                    foreach (InteractableBattery battery in playerAssignedBattery)
                    {
                        if (battery.batteryOnHand)
                        {
                            SetKeyLettersAndInstruction("E", "TO DROP OBJECTS");
                            break;
                        }
                        else
                        {
                            SetKeyLettersAndInstruction("E", "TO PICK UP OBJECTS");
                        }
                    }
                }

            }
        }

        private void InitWhiteScreen()
        {
            whiteBgInitScale = transform.localScale.y;
            whiteBG.transform.localScale = new Vector3(whiteBG.localScale.x, 0, whiteBG.localScale.z);
        }

        public void SetKeyLettersAndInstruction(string letter, string function)
        {
            keyLetters.SetText(letter);
            instructionTxt.SetText(function);
        }

        public void LineUPTxt()
        {
            keyLetters.SetText("LINE UP");
            liningUp = true;
            pressTxt.gameObject.SetActive(false);
            instructionTxt.gameObject.SetActive(false);
        }

        public void ProceedToNextRoomText()
        {
            instructionTxt.gameObject.SetActive(false);
            pressTxt.gameObject.SetActive(false);
            keyLetters.fontSize = 20;
            keyLetters.SetText("Proceed To The Next Room");
        }
    }
}
