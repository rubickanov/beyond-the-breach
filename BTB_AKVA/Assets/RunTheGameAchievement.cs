using Steamworks;
using UnityEngine;

namespace AKVA
{
    public class RunTheGameAchievement : MonoBehaviour
    {
        private void Start()
        {
            if (SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement("BTB_RUN_THE_GAME", out bool achivementCompleted);

                if (!achivementCompleted)
                {
                    SteamUserStats.SetAchievement("BTB_RUN_THE_GAME");
                    SteamUserStats.StoreStats();
                }
            }
        }
    }
}