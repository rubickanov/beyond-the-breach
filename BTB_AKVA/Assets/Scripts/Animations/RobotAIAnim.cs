using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Animations
{
    public class RobotAIAnim : MonoBehaviour
    {
        Rigidbody rb;
        Animator anim;
        public string currentAnim;
        public string Robot_Walk = "WalkForward";
        public string Robot_Idle = "Idle";
        public string Robot_DropItem = "Carry-Putdown";
        public string Robot_PickItem = "Carry-PickUp";
        public string Robot_CarryWalk = "Carry-WalkForward";
        public string Robot_Death = "Electricute";

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
        }

        public void ChangeAnimState(string newState)
        {
            if(currentAnim == newState) { return; }
            anim.Play(newState);
            currentAnim = newState;
        }
    }
}
