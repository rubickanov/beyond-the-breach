using System;
using UnityEngine;

namespace AKVA.Player
{
    public class PlayerStatus : MonoBehaviour
    {
        public EventHandler<RestartReason> OnPlayerDeath;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                KillPlayer(RestartReason.CAMERA_SPOTTED);
            }
        }

        public void KillPlayer(RestartReason restartReason)
        {
            PlayerInput.Instance.DisablePlayerInput();
            OnPlayerDeath?.Invoke(this, restartReason);
        }
    }
}
