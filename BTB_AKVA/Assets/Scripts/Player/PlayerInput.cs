using System.Runtime.CompilerServices;
using AKVA.Controls;
using UnityEngine;

namespace AKVA.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private MouseLook mouseLook;
        [SerializeField] private EagleVision eagleVision;
        
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

        public void EnablePlayerMouseInput()
        {
            mouseLook.enabled = true;
        }
        
        public void DisablePlayerMouseInput()
        {
            mouseLook.enabled = false;
        }

        public void DisablePlayerMovement()
        {
            playerMovement.enabled = false;
        }

        public void EnablePlayerMovement()
        {
            if (playerMovement.transform.TryGetComponent(out QTEEscape qte)) return;
            playerMovement.enabled = true;
        }

        public bool GetPlayerMovement => playerMovement.enabled;
        public bool IsCameraInputEnabled => mouseLook.enabled;
    }
}
