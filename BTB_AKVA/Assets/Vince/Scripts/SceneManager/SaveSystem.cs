using System.IO;
using AKVA.Assets.Vince.Scripts.SceneManager;
using UnityEngine;

namespace AKVA.Player
{
    public class SaveSystem : MonoBehaviour
    {
        private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

        public static SaveSystem Instance { get; private set; }

        public bool IsSaved => File.Exists(SAVE_FOLDER + "/save.txt");

        [SerializeField] private SavedCheckpoint savedCheckpoint;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            if (!Directory.Exists(SAVE_FOLDER))
            {
                Directory.CreateDirectory(SAVE_FOLDER);
            }
        }


        public void SaveGame(SavedCheckpoint checkpoint)
        {
            SaveObject saveObject = new SaveObject
            {
                isSaved = checkpoint.isSaved,
                position = checkpoint.position,
                forward = checkpoint.forward,
                stateEnum = checkpoint.stateEnum,
                isLastCheckPoint = checkpoint.isLastCheckPoint
            };

            string json = JsonUtility.ToJson(saveObject);

            File.WriteAllText(SAVE_FOLDER + "/save.txt", json);

            Debug.Log(json);
        }

        public void LoadGame()
        {
            if (File.Exists(SAVE_FOLDER + "/save.txt"))
            {
                string saveString = File.ReadAllText(SAVE_FOLDER + "/save.txt");

                SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

                savedCheckpoint.PassSavedValues(saveObject);
            }
            else
            {
                Debug.LogError("No save file found!");
            }
        }
    }
}

public class SaveObject
{
    public bool isSaved = false;
    public Vector3 position;
    public Vector3 forward;
    public LevelManager.LevelStatesEnum stateEnum;
    public bool isLastCheckPoint = false;
}