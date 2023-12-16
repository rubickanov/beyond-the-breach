using System;
using EZCameraShake;
using UnityEngine;
using AKVA.Assets.Vince.Scripts.SceneManager;

namespace AKVA.Player
{
    public class PlayerCheckpointManager : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform playerCameraTransform;
        [SerializeField] private SavedCheckpoint savedCheckpoint;
        [SerializeField] private GameIntro gameIntro;
        [SerializeField] private bool enableCheckpoint = true;

        [SerializeField] private GameObject scientist;
        [SerializeField] private GameObject redCircle1;
        [SerializeField] private GameObject redCircle2;

        [SerializeField] GameObject[] sceneLevels; 

        private LevelManager levelManager;
        
        private void Awake()
        {
            levelManager = GetComponent<LevelManager>();
            if (enableCheckpoint)
            {
                EnableLastActiveLevel();
                StartGameOnSavedCheckpoint();
            }
        }

        private void Start()
        {
            if (savedCheckpoint.isLastCheckPoint)
            {
                HandleLastCheckPoint();
            }

            if (!PlayerPrefs.HasKey("MainSceneUnlocked"))
            {
                PlayerPrefs.SetInt("MainSceneUnlocked", 1);
            }
        }

        private void StartGameOnSavedCheckpoint()
        {
            if (!savedCheckpoint.isSaved) return;

            gameIntro.enabled = false;

            playerTransform.position = savedCheckpoint.position;
            playerTransform.forward = savedCheckpoint.forward;
            levelManager.SetCurrentLevel(savedCheckpoint.stateEnum);

            playerCameraTransform.GetComponent<CameraShaker>().enabled = false;
            playerTransform.GetComponent<QTEEscape>().enabled = false;
            playerTransform.GetComponent<QTEEscape>().DisableUI();
            playerTransform.GetComponent<EagleVision>().qteActivate = false;
        }

        private void HandleLastCheckPoint()
        {
            scientist.SetActive(false);
            redCircle1.SetActive(false);
            redCircle2.SetActive(false);
        }

        void EnableLastActiveLevel()
        {
            switch (savedCheckpoint.stateEnum)
            {
                case LevelManager.LevelStatesEnum.Level1:
                    sceneLevels[0].SetActive(true);
                    break;
                case LevelManager.LevelStatesEnum.Level2 :
                    sceneLevels[0].SetActive(true);
                    sceneLevels[1].SetActive(true);
                    break;
                case LevelManager.LevelStatesEnum.Level3:
                    sceneLevels[0].SetActive(false);
                    sceneLevels[1].SetActive(true);
                    sceneLevels[2].SetActive(true);
                    break;
                case LevelManager.LevelStatesEnum.Level4:

                    sceneLevels[0].SetActive(false);
                    sceneLevels[1].SetActive(false);

                    sceneLevels[2].SetActive(true);
                    sceneLevels[3].SetActive(true);
                    break;
                case LevelManager.LevelStatesEnum.Level5:

                    sceneLevels[0].SetActive(false);
                    sceneLevels[1].SetActive(false);
                    sceneLevels[2].SetActive(false);

                    sceneLevels[3].SetActive(true);
                    sceneLevels[4].SetActive(true);
                    break;
                case LevelManager.LevelStatesEnum.Level6:

                    sceneLevels[0].SetActive(false);
                    sceneLevels[1].SetActive(false);
                    sceneLevels[3].SetActive(false);
                    sceneLevels[2].SetActive(false);

                    sceneLevels[4].SetActive(true);
                    sceneLevels[5].SetActive(true);
                    break;

                case LevelManager.LevelStatesEnum.Level7:
                    sceneLevels[0].SetActive(false);
                    sceneLevels[1].SetActive(false);
                    sceneLevels[2].SetActive(false);
                    sceneLevels[3].SetActive(false);

                    sceneLevels[4].SetActive(true);
                    sceneLevels[5].SetActive(true);
                    break;
            }
        }
    }
}