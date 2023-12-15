using AKVA.Assets.Vince.Scripts.SceneManager;
using UnityEngine;
using UnityEngine.Serialization;

namespace AKVA.Player
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private SavedCheckpoint savedCheckpoint;
        [SerializeField] private LevelManager.LevelStatesEnum levelState;
        [SerializeField] private bool isLastCheckpoint = false;
        [SerializeField] GameObject [] levelsToDisable;

        void DisableALevel()
        {
            if(levelsToDisable.Length > 0)
            {
                foreach(GameObject level in levelsToDisable)
                {
                    level.SetActive(false);
                }
            }   
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                LevelManager.Instance.SetCurrentLevel(levelState);
                DisableALevel();
                savedCheckpoint.position = transform.position;
                savedCheckpoint.forward = transform.forward;
                savedCheckpoint.isSaved = true;
                savedCheckpoint.stateEnum = levelState;
                savedCheckpoint.isLastCheckPoint = isLastCheckpoint;
            }
        }
    }
}