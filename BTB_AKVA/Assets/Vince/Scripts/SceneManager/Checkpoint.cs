using AKVA.Assets.Vince.Scripts.SceneManager;
using AKVA.Player;
using UnityEngine;

namespace AKVA
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private SavedCheckpoint savedCheckpoint;
        [SerializeField] private LevelManager.LevelStatesEnum levelState;
        [SerializeField] private bool isLastCheckpoint = false;
        [SerializeField] GameObject [] levelsToDisable;
        bool paHasPlayed;
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
                EnablePASystem();
                DisableALevel();
                savedCheckpoint.position = transform.position;
                savedCheckpoint.forward = transform.forward;
                savedCheckpoint.isSaved = true;
                savedCheckpoint.stateEnum = levelState;
                savedCheckpoint.isLastCheckPoint = isLastCheckpoint;
                
                SaveSystem.Instance.SaveGame(savedCheckpoint);
            }
        }

        void EnablePASystem()
        {
            if (paHasPlayed) { return; }
            switch (levelState)
            {
                case LevelManager.LevelStatesEnum.Level2:
                    SubtitleManager.Instance.PlayPublicAnnoucememnt("PA System (Overhead speaker):","Serial Number 1YQ4X-R18WW, please report to Facility Room No. 8 immediately.", 0, 7f);
                    paHasPlayed = true;
                    break;
            }
        }
    }
}