using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AKVA
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] bool applyFadeOutEffect;
        [SerializeField] Image blackBG;
        [SerializeField] float fadeOutSpeed = 0.2f;
        [SerializeField] private bool useBGFade = true;
        [SerializeField] private float loadSceneTime;
        float currentAlpha;
        bool enableFadeOut;


        public static SceneLoader Instance;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            loadingScreen.SetActive(false);
            
            Time.timeScale = 1;
            if (useBGFade)
            {
                AdjustImageAlpha(0f);
            }
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


        public void LoadLevelWithLoadingScreen(int sceneIndex)
        {
            if (sceneIndex != 0)
            {
                Cursor.visible = false;
            }
            
            loadingScreen.SetActive(true);
            
            SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}
