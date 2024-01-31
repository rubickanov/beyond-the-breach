using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AKVA.Steam
{
    public class SteamHandler : MonoBehaviour
    {
        private void Awake()
        {
            try
            {
                Steamworks.SteamClient.Init(2752930);
            }
            catch(System.Exception e)
            {
                Debug.LogError($"STEAM HANDLER: {e}");
            }
        }

        private void Start()
        {
            Application.quitting += ApplicationOnquitting;
        }

        private void Update()
        {
            Steamworks.SteamClient.RunCallbacks();
        }
        private void ApplicationOnquitting()
        {
            Steamworks.SteamClient.Shutdown();
        }
    }
}
