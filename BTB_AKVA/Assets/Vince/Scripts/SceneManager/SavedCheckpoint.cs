using AKVA.Assets.Vince.Scripts.SceneManager;
using log4net.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace AKVA.Player
{
    [CreateAssetMenu()]
    public class SavedCheckpoint : ScriptableObject
    {
        public Vector3 position;
        public Vector3 forward;

        public bool isSaved = false;

        public LevelManager.LevelStatesEnum stateEnum;

        public bool isLastCheckPoint = false;

        public void ResetCheckpoint()
        {
            position = Vector3.zero;
            forward = Vector3.zero;
            isSaved = false;
            stateEnum = LevelManager.LevelStatesEnum.Level1;
        }
    }
}
