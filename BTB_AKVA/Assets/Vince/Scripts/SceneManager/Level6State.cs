using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Level6State : LevelState
    {
        LevelManager levelManager;
        bool musicHasPlayed;
        public override void OnEnterState(LevelManager state)
        {
            levelManager = state;
            foreach (GameObject scientist in state.scientistsLevel6)
            {
                scientist.SetActive(true);
            }
            PlayLevelMusic();
        }

        public override void OnUpdateState(LevelManager state)
        {
        }

        void PlayLevelMusic()
        {
            if(!levelManager.level6MusicPlayed)
            {
                levelManager.level6MusicPlayed = true;
                levelManager.musicAudioSource.Stop();
                levelManager.musicAudioSource.clip = levelManager.musicToPlay;
                levelManager.musicAudioSource.Play();
            }
        }
    }
}
