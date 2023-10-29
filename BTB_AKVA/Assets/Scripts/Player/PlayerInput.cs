using AKVA.Controls;
using UnityEngine;

namespace AKVA.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private MouseLook mouseLook;
        
        public static PlayerInput Instance { get; private set; }

        [SerializeField] public ControlsSO Controls;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void EnablePlayerInput()
        {
            playerMovement.enabled = true;
            mouseLook.enabled = true;
        }

        public void DisablePlayerInput()
        {
            playerMovement.enabled = false;
            mouseLook.enabled = false;
        }

        public void DisablePlayerMovement(bool enable)
        {
            playerMovement.enabled = !enable;
        }

        public bool GetPlayerMovement => playerMovement.enabled;
    }
}
