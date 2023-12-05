using UnityEngine;
using AKVA.Player;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AKVA.GameplayUI
{
    public class RestartScreen : MonoBehaviour
    {
        [SerializeField] private PlayerStatus playerStatus;
        [SerializeField] private TextMeshProUGUI deathReasonText;
        [SerializeField] private Button restartButton;

        [SerializeField] private string DEFAULT_REASON_TEXT;
        [SerializeField] private string CAMERA_SPOTTED_TEXT;
        [SerializeField] private string TEST_REASON_TEXT;
        [SerializeField] private string ANOTHER_TEST_REASON_TEXT;
        
        private void Start()
        {
            gameObject.SetActive(false);
            
            playerStatus.OnPlayerDeath += ShowRestartScreen;
            restartButton.onClick.AddListener(RestartScene);
        }

        private void ShowRestartScreen(object sender, RestartReason e)
        {
            gameObject.SetActive(true);
            deathReasonText.text = GetReasonText(e);
            Cursor.lockState = CursorLockMode.None;
        }

        private string GetReasonText(RestartReason reason)
        {
            return reason switch
            {
                RestartReason.CAMERA_SPOTTED => CAMERA_SPOTTED_TEXT,
                RestartReason.TEST_RESTART => TEST_REASON_TEXT,
                RestartReason.ANOTHER_TEST_RESTART => ANOTHER_TEST_REASON_TEXT,
                _ => DEFAULT_REASON_TEXT
            };
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}