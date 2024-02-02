using AKVA.Player;
using System.Collections;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOutEnding : MonoBehaviour
{
    [SerializeField] Image bgImage;
    [SerializeField] private Image titleLogo;
    [SerializeField] private TextMeshProUGUI thxText;
    [SerializeField] float fadeInTime = 0.5f;
    [SerializeField] private float thxTextFadeInTime = 0.25f;
    [SerializeField] private GameObject subGameObject;
    float currentAlpha;
    private void OnEnable()
    {
        GetAchievement();
        StartCoroutine(FadeInBlackImage());
    }

    private void GetAchievement()
    {
        if (SteamManager.Initialized)
        {
            SteamUserStats.GetAchievement("BTB_COMPLETE_THE_GAME", out bool achievementCompleted);

            if (!achievementCompleted)
            {
                SteamUserStats.SetAchievement("BTB_COMPLETE_THE_GAME");
                SteamUserStats.StoreStats();
            }
        }
    }

    IEnumerator FadeInBlackImage()
    {
        subGameObject.SetActive(false);
        while (bgImage.color.a < 1)
        {
            yield return null;

            PlayerInput.Instance.DisablePlayerInput();

            currentAlpha += fadeInTime * Time.deltaTime;

            bgImage.color = new Color(bgImage.color.r, bgImage.color.g, bgImage.color.b, currentAlpha);
            //titleLogo.color = new Color(titleLogo.color.r, titleLogo.color.g, titleLogo.color.b, currentAlpha);
        }
        
        currentAlpha = 0;
        yield return new WaitForSeconds(2.0f);

        while (thxText.color.a < 1)
        {
            yield return null;

            currentAlpha += thxTextFadeInTime * Time.deltaTime;

            thxText.color = new Color(thxText.color.r, thxText.color.g, thxText.color.b, currentAlpha);
        }

        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(0);
    }
}
