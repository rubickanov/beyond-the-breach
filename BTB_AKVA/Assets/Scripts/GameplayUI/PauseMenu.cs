using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("MENUS")] 
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject optionsUI;
    
    [Header("BUTTONS")] 
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;
    
    private bool isPaused;
    
    private void Start()
    {

        resumeButton.onClick.AddListener(ResumeGame);
        
        restartButton.onClick.AddListener(RestartGame);
        
        optionsButton.onClick.AddListener(ShowOptionsMenu);
        
        exitButton.onClick.AddListener(QuitToMainMenu);
        
        ResumeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    private void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        pauseUI.SetActive(false);
        optionsUI.SetActive(false);
        background.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        pauseUI.SetActive(true);
        background.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ShowOptionsMenu()
    {
        pauseUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    private void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
