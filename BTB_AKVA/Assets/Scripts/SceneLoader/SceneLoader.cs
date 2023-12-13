using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AKVA
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] bool applyFadeOutEffect;
        [SerializeField] Image blackBG;
        [SerializeField] float fadeOutSpeed = 0.2f;
        float currentAlpha;
        bool enableFadeOut;
        private void Awake()
        {
            AdjustImageAlpha(0f);
        }
        public void LoadScene(string sceneName)
        {
            if (applyFadeOutEffect)
            {
                StartCoroutine(FadeOutEffect(sceneName));
            }
            else
            {
                SceneManager.LoadScene(sceneName);

            }
        }

        IEnumerator FadeOutEffect(string sceneName)
        {
            while(blackBG.color.a < 1)
            {
                yield return null;
                currentAlpha += fadeOutSpeed * Time.deltaTime;
                AdjustImageAlpha(currentAlpha);
            }
            SceneManager.LoadScene(sceneName);
        }

        void AdjustImageAlpha(float value)
        {
            blackBG.color = new Color(blackBG.color.r, blackBG.color.g, blackBG.color.b, value);
        }
    }
}
