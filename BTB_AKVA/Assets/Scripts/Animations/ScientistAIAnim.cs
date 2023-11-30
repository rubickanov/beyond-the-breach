using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Animations
{
    public class ScientistAIAnim : MonoBehaviour
    {
        Animator anim;
        public string startingAnim;
        public string currentAnim;
        public string Robot_Walk = "WalkForward";
        public string Robot_Idle = "Idle";
        public string Robot_Interaction = "InteractionWithMachine";
        public string Robot_Interaction2= "InteractionWithMachine2";
        public string Robot_Texting = "Texting";

        private void Start()
        {
            anim = GetComponent<Animator>();
            ChangeAnimState(startingAnim);
        }

        public void ChangeAnimState(string newState)
        {
            if (currentAnim == newState) { return; }
            anim.Play(newState);
            currentAnim = newState;
        }
    }
}
