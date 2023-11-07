using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.SceneManager;
using AKVA.Player;
using PlasticGui.WorkspaceWindow.BranchExplorer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;


namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Room3State : SceneState
    {
        bool playerInPosition;
        bool aiActive; //Initiates the AI task
        bool[] task; //Initiate Each AI to be activated
        bool aiEnabled;


        public override void OnEnterState(SceneStateManager state)
        {
            Debug.Log("Room State 3!");
            state.playerPicking.enabled = false;
            task = new bool[2];
        }

        public override void OnExitState(SceneStateManager state)
        {
        }

        public override void OnUpdateState(SceneStateManager state)
        {
            CheckIfPlayerIsInThePlaceHolder(state);
            AITask(state);
        }

        private void CheckIfPlayerIsInThePlaceHolder(SceneStateManager state)
        {
            if (Vector3.Distance(state.playerTransform.position, state.room3PlayerPos.position) < 1.5f && !playerInPosition) // if player has positioned to its place holder
            {
                Debug.Log("Player is In position");
                PlayerInput.Instance.DisablePlayerMovement(true);
                state.tvTurnedOn.value = true;
                state.imagesAppeared[0].value = true;
                state.StartCoroutine(StartAITask(state, 0, 4.5f, false));
                playerInPosition = true;
            }
        }

        void AITask(SceneStateManager state)
        {
            if (aiActive)
            {
                if (Vector3.Distance(state.listOfAI.items[0].transform.position, state.aiDestination[0].position) < 2.5f && !task[0] && !aiEnabled)
                {
                    state.imagesAppeared[1].value = true;
                    state.StartCoroutine(StartAITask(state, 0, 4.5f, true));
                    aiEnabled = true;
                }

                if (task[0] && !task[1])
                {
                    state.imagesAppeared[2].value = true;
                    task[1] = true;
                }
            }
        }

        IEnumerator StartAITask(SceneStateManager state, int aiIndex, float delayTime, bool enable) //Starting AI to do its task
        {
            yield return new WaitForSeconds(delayTime);
            state.listOfAI.items[aiIndex].GetComponent<AIStateManager>().activateAI = true;
            aiActive = true;

            if (enable)// to enable image after showing the pass key
            {
                task[0] = true;
            }
        }
    }
}
