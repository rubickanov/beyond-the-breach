using UnityEngine;
using AKVA.GameplayUI;

namespace AKVA.Interaction
{
    public class ReticleHandler : MonoBehaviour
    {
        [SerializeField] private int secondsToDeactivate;
        [SerializeField] private ReticleUI reticleUI;
        private MindControl mindControl;
        private Interaction interaction;
        private Picking picking;

        private float timer = 0;

        private void Awake()
        {
            mindControl = GetComponent<MindControl>();
            interaction = GetComponent<Interaction>();
            picking = GetComponent<Picking>();
        }

        private void Update()
        {
            
            if (!mindControl.IsActive && !picking.IsActive && !interaction.IsActive)
            {
                reticleUI.SetDefaultUI();
                timer += Time.deltaTime;
                if (timer >= secondsToDeactivate)
                {
                    if (reticleUI.IsEnabled)
                    {
                        reticleUI.DisableReticle();
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
                if (mindControl.IsActive)
                {
                 reticleUI.SetMindControlUI();   
                } else if (picking.IsActive || interaction.IsActive)
                {
                    reticleUI.SetInteractionUI();
                }
            }
        }
    }
}
