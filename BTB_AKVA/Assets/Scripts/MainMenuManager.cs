using System;
using System.Collections;
using System.Collections.Generic;
using AKVA.Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AKVA
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button continueButton; 
        
        [SerializeField] private FloatReference bgMusicVolume;

        [SerializeField] private bool ImitateFirstTime;

        private void Start()
        {
            quitButton.onClick.AddListener(QuitGame);


            if (!PlayerPrefs.HasKey("MainSceneUnlocked") || ImitateFirstTime)
            {
                Debug.Log("PLAYER PREF HAS NOT BEEN FOUND");
                continueButton.interactable = false;
                continueButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
                continueButton.GetComponent<EventTrigger>().enabled = false;
            }
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
