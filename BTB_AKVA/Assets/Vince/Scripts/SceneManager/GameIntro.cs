using AKVA.Player;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class GameIntro : MonoBehaviour
{
    [SerializeField] Image blackBG;
    [SerializeField] GameObject warningLogo;
    [SerializeField] GameObject escapeSlider;
    [SerializeField] QTEEscape qteEscapeScript;
    [SerializeField] float gameStartDelay = 3f;
    [SerializeField] float fadeOutSpeed = 0.4f;
    float currentAlpha = 1;

    private void OnEnable()
    {
        blackBG.color = new Color(blackBG.color.r, blackBG.color.g, blackBG.color.b, 1);
        qteEscapeScript.enabled = false;
        escapeSlider.SetActive(false);
        StartCoroutine(FadeOutBlackBG());
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            PlayerInput.Instance.DisablePlayerInput();
        }
    }

    IEnumerator FadeOutBlackBG()
    {
        yield return new WaitForSeconds(gameStartDelay);
        warningLogo.SetActive(true);

        yield return new WaitForSeconds(gameStartDelay);

        while (blackBG.color.a > 0)
        {
            yield return null;
            currentAlpha -= fadeOutSpeed * Time.deltaTime;
            blackBG.color = new Color(blackBG.color.r, blackBG.color.g, blackBG.color.b, currentAlpha);
        }
        blackBG.gameObject.SetActive(false);
        escapeSlider.SetActive(true);
        qteEscapeScript.enabled = true;
    }
}
