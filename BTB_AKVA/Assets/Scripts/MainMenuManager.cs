using AKVA.Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AKVA
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject optionsUI;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button skipTutorialButton;
        [SerializeField] private Button continueButton;
        
        [SerializeField] private FloatReference bgMusicVolume;

        [SerializeField] private bool ImitateFirstTime;
        [SerializeField] Material materialToChange;

        private void Start()
        {
            materialToChange.color = Color.white;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            optionsUI.SetActive(false);
            quitButton.onClick.AddListener(QuitGame);


            if (!PlayerPrefs.HasKey("MainSceneUnlocked"))
            {
                Debug.Log("PLAYER PREF HAS NOT BEEN FOUND");
                skipTutorialButton.interactable = false;
                skipTutorialButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                skipTutorialButton.GetComponent<EventTrigger>().enabled = false;
            }

            if (!SaveSystem.Instance.IsSaved)
            {
                continueButton.interactable = false;
                continueButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                continueButton.GetComponent<EventTrigger>().enabled = false;
            }
        }
        
        public void LoadSavedGame()
        {
            SaveSystem.Instance.LoadGame();
            SceneManager.LoadScene(2);
        }

        private void Update()
        {
            audioSource.volume = bgMusicVolume.value / 100f;
        }

        

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}
