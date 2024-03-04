using System;
using AKVA.Assets.Vince.Scripts.SceneManager;
using UnityEngine;
using UnityEngine.Windows;

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
        
        public void PassSavedValues(SaveObject saveObject)
        {
            position = saveObject.position;
            forward = saveObject.forward;
            isSaved = saveObject.isSaved;
            stateEnum = saveObject.stateEnum;
            isLastCheckPoint = saveObject.isLastCheckPoint;
        }
    }
}
