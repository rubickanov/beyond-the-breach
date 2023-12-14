using System;
using System.Collections;
using System.Collections.Generic;
using AKVA.Player;
using UnityEngine;
using UnityEngine.UI;

namespace AKVA
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button quitButton;

        [SerializeField] private FloatReference bgMusicVolume;

        private void Start()
        {
            quitButton.onClick.AddListener(QuitGame);
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
