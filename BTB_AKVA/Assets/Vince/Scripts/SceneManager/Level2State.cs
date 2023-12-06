using AKVA.Assets.Vince.Scripts.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Level2State : LevelState
    {
        public override void OnEnterState(LevelManager state)
        {
            state.scientistLevel2.SetActive(true);
        }

        public override void OnUpdateState(LevelManager state)
        {
        }

       
    }
}
