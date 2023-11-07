using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI keyLetters;
        [SerializeField] TextMeshProUGUI functionTxt;
        [SerializeField] TextMeshProUGUI pressTxt;
        [SerializeField] Transform whiteBG;
        [HideInInspector] public bool turnOnTV;
        float whiteBgInitScale;

        void Start ()
        {
            InitWhiteScreen();
        }

        private void Update()
        {
            if(turnOnTV)
            {
                if (whiteBG.localScale.y < whiteBgInitScale)
                {
                    float currentScale = Mathf.Lerp(whiteBG.localScale.y, whiteBgInitScale, 6 * Time.deltaTime);
                    whiteBG.localScale = new Vector3(whiteBG.localScale.x, currentScale, whiteBG.localScale.z);
                }
            }
        }

        private void InitWhiteScreen()
        {
            whiteBgInitScale = transform.localScale.y;
            whiteBG.transform.localScale = new Vector3(whiteBG.localScale.x, 0, whiteBG.localScale.z);
        }

        public void SetKeyLettersAndInsruction(string letter, string function)
        {
            keyLetters.SetText(letter);
            functionTxt.SetText(function);
        }

        public void ProceedToNextRoomText()
        {
            functionTxt.gameObject.SetActive(false);
            pressTxt.gameObject.SetActive(false);
            keyLetters.fontSize = 20;
            keyLetters.SetText("Proceed To The Next Room");
        }
    }
}