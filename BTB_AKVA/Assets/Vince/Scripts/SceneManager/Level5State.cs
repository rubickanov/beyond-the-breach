using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.SceneManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace Assets.Vince.Scripts.SceneManager
{
    public class Level5State : LevelState
    {
        RobotAIAnim anim;
        bool switchEnabled;
        public override void OnEnterState(LevelManager state)
        {
            Debug.Log("Level 5");
            state.scientistLevel5.SetActive(true);
            anim = state.robotAnim;

        }

        public override void OnUpdateState(LevelManager state)
        {
            switchEnabled = state.switchStatus.value;
            SetRobotAnimation();
        }

        void SetRobotAnimation()
        {
            if (switchEnabled)
            {
                anim.ChangeAnimState("ElectricuteLoop");
            }
            else
            {
                anim.ChangeAnimState(anim.Robot_Idle);
            }
        }

        
    }
}