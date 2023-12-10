using UnityEngine;
using AKVA.GameplayUI;

namespace AKVA.Interaction
{
    public class ReticleHandler : MonoBehaviour
    {
        [SerializeField] private bool isPermanentReticle;
        [SerializeField] private int secondsToDeactivate;
        [SerializeField] private ReticleUI reticleUI;
        
        private MindControl mindControl;
        private Interaction interaction;
        private Picking picking;
        private PassDeviceChecker passDeviceChecker;

        private float timer = 0;

        private void Awake()
        {
            mindControl = GetComponent<MindControl>();
            interaction = GetComponent<Interaction>();
            picking = GetComponent<Picking>();
            passDeviceChecker = GetComponent<PassDeviceChecker>();
        }

        private void Start()
        {
            if (!isPermanentReticle)
            {
                reticleUI.DisableReticleImmediately();
            }
        }

        private void Update()
        {
            
            if (!mindControl.IsActive && !picking.IsActive && !interaction.IsActive && !passDeviceChecker.IsActive)
            {
                reticleUI.SetDefaultUI();
                if(!isPermanentReticle)
                {
                    timer += Time.deltaTime;
                    if (timer >= secondsToDeactivate)
                    {
                        if (reticleUI.IsEnabled)
                        {
                            reticleUI.DisableReticle();
                        }
                    }
                }
            }
            else
            {
                timer = 0;
                if (!reticleUI.IsEnabled)
                {
                    reticleUI.EnableReticle();
            
                }
                if (mindControl.IsActive && !mindControl.IsUnHacking)
                {
                 reticleUI.SetMindControlUI();   
                } else if (picking.IsActive || interaction.IsActive)
                {
                    reticleUI.SetInteractionUI();
                } else if (passDeviceChecker.IsActive)
                {
                    reticleUI.SetPasswordReticle();
                } else if (mindControl.IsActive && mindControl.IsUnHacking)
                {
                    reticleUI.SetUnMindControlUI();
                }
            
            }
        }
    }
}
