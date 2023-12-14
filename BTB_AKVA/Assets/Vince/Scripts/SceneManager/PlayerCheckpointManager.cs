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

        [SerializeField] private bool enableCheckpoint = true;

        [SerializeField] private GameObject scientist;
        [SerializeField] private GameObject redCircle1;
        [SerializeField] private GameObject redCircle2;

        private LevelManager levelManager;
        
        private void Awake()
        {
            levelManager = GetComponent<LevelManager>();
            
            if (enableCheckpoint)
            {
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
                PlayerPrefs.Save();
            }
        }

        private void StartGameOnSavedCheckpoint()
        {
            if (!savedCheckpoint.isSaved) return;


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
    }
}