using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace AKVA
{
    public class TutorialUIManager : MonoBehaviour
    {
        [SerializeField] GameObject tutorialPanel;
        [SerializeField] Button closeBtn;
        [SerializeField] VideoPlayer tutorialVideoPlayer;

        void Start()
        {
            closeBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1f; 
                tutorialPanel.SetActive(false); 
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                });
        }

        public void ActivateTutorialUI()
        {
            Time.timeScale = 0f;
            tutorialVideoPlayer.Play();
            tutorialPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
