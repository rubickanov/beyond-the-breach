using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button restartButton;
    

    private void Start()
    {
        restartButton = GetComponent<Button>();

        restartButton.onClick.AddListener(RestartLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartLevel();
        }
    }


    private void RestartLevel()
    {
        PlayerDeathManager.Instance.ResetPlayerDied();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
