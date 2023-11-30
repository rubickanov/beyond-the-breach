using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering;

namespace AKVA.Animations
{
    public class RobotAIAnim : MonoBehaviour
    {
        Animator anim;
        string currentState;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ChangeAnimState(string newState)
        {
            if(currentState == newState) { return; }
            anim.Play(newState);
            currentState = newState;
        }
    }
}
