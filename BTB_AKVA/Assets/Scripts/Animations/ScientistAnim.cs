using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Animations
{
    public class ScientistAnim : MonoBehaviour
    {
        Animator anim;
        public string currentAnim;
        public string Walk = "WalkForward";
        public string Idle = "Idle";
        public string MachineInteraction = "MachineInteraction";
        public string MachineInteraction2 = "MachineInteraction2";
        public string Texting = "Texting";

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void ChangeAnimState(string newState)
        {
            if (currentAnim == newState) { return; }

            if(currentAnim == MachineInteraction || currentAnim == Texting || currentAnim == MachineInteraction2)
            {
                anim.applyRootMotion = true;
            }
            else
            {
                anim.applyRootMotion = false;
            }

            anim.Play(newState);
            currentAnim = newState;
        }
    }
}
