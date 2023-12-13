using AKVA.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutEnding : MonoBehaviour
{
    [SerializeField] Image bgImage;
    [SerializeField] float fadeInTime = 0.5f;
    float currentAlpha;
    private void OnEnable()
    {
        StartCoroutine(FadeInBlackImage());
    }

    IEnumerator FadeInBlackImage()
    {
        while (bgImage.color.a < 1)
        {
            yield return null;

            PlayerInput.Instance.DisablePlayerInput();

            currentAlpha += fadeInTime * Time.deltaTime;

            bgImage.color = new Color(bgImage.color.r, bgImage.color.g, bgImage.color.b, currentAlpha);

        }
    }
}
