using AKVA.Assets.Vince.Scripts.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Level2State : LevelState
    {
        ScientistStateManager scientistStateManager;
        Transform aiTransform;
        public override void OnEnterState(LevelManager state)
        {
            scientistStateManager = state.level2AI.GetComponent<ScientistStateManager>();
            aiTransform = state.level2AI.transform;
            scientistStateManager.activate = true;
        }

        public override void OnUpdateState(LevelManager state)
        {
            RespawnAI(state);
        }

        private void RespawnAI(LevelManager state)
        {
            if(Vector3.Distance(aiTransform.position, state.aIEndPos.position) < 4)
            {
                scientistStateManager.targetIndex = 0;
                scientistStateManager.moveOnly = false;
                aiTransform.position = state.aIStartPos.position;
                state.SwitchState(state.level2);
            }
        }
    }
}
