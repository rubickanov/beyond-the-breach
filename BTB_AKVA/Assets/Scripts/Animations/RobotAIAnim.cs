using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AKVA.Animations
{
    public class RobotAIAnim : MonoBehaviour
    {
        Rigidbody rb;
        Animator anim;
        public string startingAnim;
        public string currentAnim;
        public string Robot_Walk = "WalkForward";
        public string Robot_Run = "run";
        public string Robot_Idle = "Idle";
        public string Robot_DropItem = "Carry-Putdown";
        public string Robot_PickItem = "Carry-PickUp";
        public string Robot_CarryWalk = "Carry-WalkForward";
        public string Robot_Scared = "LookAroundScared";
        public string Robot_Death = "Electricute";

        public UnityEvent OnWalk;
        public UnityEvent Electricuted;
       

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            ChangeAnimState(startingAnim);
        }

        public void ChangeAnimState(string newState)
        {
            if(currentAnim == newState) { return; }

            if(currentAnim == newState) { anim.applyRootMotion = true; } else {  anim.applyRootMotion = false; }
            anim.Play(newState);
            currentAnim = newState;
        }

        public void FootStep()
        {
            OnWalk.Invoke();
        }

        public void Electricute()
        {
            Electricuted.Invoke();
        }
    }
}
