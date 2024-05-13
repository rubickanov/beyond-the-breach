using AKVA.Assets.Vince.Scripts.Environment;
using AKVA.Dialogue;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AKVA
{
    public class CodeGenerator : MonoBehaviour
    {

        [SerializeField] PasswordDevice passDevice;
        [SerializeField] string newCode;
        [SerializeField] int numOfChar = 4;
        [SerializeField] TextMeshProUGUI [] codeTxt;
        int startIndex;
        int endIndex;

        private void Start()
        {
            SetCodeTxtForComputer();
        }

        private void SetCodeTxtForComputer()
        {
            if (codeTxt.Length <= 0) return;
            RandomizeCode();

            for (int i = 0; i < codeTxt.Length; i++)
            {
                codeTxt[i].text = newCode;
            }
            passDevice.devicePassword = newCode;
        }

        public void GenerateNewCodeForDialouge(Sentence[] sentences, DialogueSO dialogue)
        {
            sentences = dialogue.Sentences;

            RandomizeCode();

            foreach (Sentence sentence in sentences)
            {
                string characterSentence = sentence.sentenceText;

                startIndex = characterSentence.IndexOf('(');
                endIndex = characterSentence.IndexOf(')');

                if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                {
                    characterSentence = characterSentence.Substring(0, startIndex + 1) + newCode + characterSentence.Substring(endIndex);

                    // Update the original sentence with the modified characterSentence
                    sentence.sentenceText = characterSentence;
                }
            }

            passDevice.devicePassword = newCode;
        }

        void RandomizeCode()
        {
            for (int i = 0; i < numOfChar; i++)
            {
                newCode += RandomizeNumber().ToString();
            }
        }

        int RandomizeNumber()
        {
            int generatedNum = Random.Range(0, 9);
            return generatedNum;
        }


    }
}
