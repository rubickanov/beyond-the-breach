using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AKVA.Player;
using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Environment;
using PlasticGui.WorkspaceWindow;
using UnityEditor;
using UnityEditor.Build;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Room1State : SceneState
    {
        bool playerInPosition;
        bool aiActive; //Initiates the AI task
        bool enableAI; //Initiate Each AI to be activated
        bool[] taskDone;
        public override void OnEnterState(SceneStateManager state)
        {
            Debug.Log("Room 1 State");
            taskDone = new bool[5];
        }

        public override void OnUpdateState(SceneStateManager state)
        {
            CheckIfPlayerIsInThePlaceHolder(state);
            ActivateAI(state);
        }

        public override void OnExitState(SceneStateManager state)
        {
        }

        private void CheckIfPlayerIsInThePlaceHolder(SceneStateManager state)
        {
            Debug.Log(Vector3.Distance(state.playerTransform.position, state.room1PlayerPos.position));
            if (Vector3.Distance(state.playerTransform.position, state.room1PlayerPos.position) < 1.5f && !playerInPosition) // if player has positioned to its place holder
            {
                Debug.Log("Player is In position");
                PlayerInput.Instance.DisablePlayerMovement();
                state.StartCoroutine(StartAITask(state, 0));
                playerInPosition = true;
            }
        }

        private void ActivateAI(SceneStateManager state)
        {
            if (aiActive)
            {
                if (GetNumberOfActiveSockets(state) == 1 && !taskDone[0] && !enableAI)
                {
                    state.StartCoroutine(StartAITask(state, 1));
                    enableAI = true;
                    taskDone[0] = true;
                }
                else if (GetNumberOfActiveSockets(state) == 2 && !taskDone[1] && !enableAI)
                {
                    state.StartCoroutine(StartAITask(state, 2));
                    enableAI = true;
                    taskDone[1] = true;
                }
                else if (GetNumberOfActiveSockets(state) == 3 && !taskDone[2] && !enableAI)
                {
                    state.playerPicking.enabled = true;
                    PlayerInput.Instance.EnablePlayerMovement();
                    taskDone[2] = true;
                }
                if (GetNumberOfActiveSockets(state) == 4 && Vector3.Distance(state.playerTransform.position, state.room1PlayerPos.position) < 1.5f && !taskDone[4])
                {
                    state.room1Door.EnableDoor = true;
                    for (int i = 0; i < state.listOfAI.Count; i++)
                    {
                        AIStateManager ai = state.listOfAI.items[i].GetComponent<AIStateManager>();
                        ai.targetIndex++;
                        ai.moveOnly = true;
                        ai.currentTarget = ai.targets[ai.targetIndex];
                        state.StartCoroutine(ProceedToNextRoom(state, ai));
                    }
                    taskDone[4] = true;
                }
                
            }
        }


        IEnumerator StartAITask(SceneStateManager state, int aiIndex) //Starting AI to do its task
        {
            yield return new WaitForSeconds(3f);
            state.listOfAI.items[aiIndex].GetComponent<AIStateManager>().activateAI = true;
            aiActive = true;
            enableAI = false;
        }

        public int GetNumberOfActiveSockets(SceneStateManager state)
        {
            int activeSockets = 0;

            foreach (var socket in state.room1Buttons.Items)
            {
                if (socket.GetComponent<BatterySocket>().socketIsActive)
                {
                    activeSockets++;
                }
            }
            return activeSockets;
        }


        IEnumerator ProceedToNextRoom(SceneStateManager state, AIStateManager ai)
        {
            yield return new WaitForSeconds(2f);
            ai.SwitchState(ai.moveState);
            state.SwitchState(state.room2State);
        }
    }
}