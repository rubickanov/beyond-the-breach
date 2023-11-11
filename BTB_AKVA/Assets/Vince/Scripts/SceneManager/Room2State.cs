using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Environment;
using AKVA.Player;
using System;
using System.Collections;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Room2State : SceneState
    {
        bool playerInPosition;
        bool aiActive; //Initiates the AI task
        bool enableAI; //Initiate Each AI to be activated
        bool[] taskDone;
        bool rotated;
        bool disableMoveOnly;

        public override void OnEnterState(SceneStateManager state)
        {
            Debug.Log("Room 2 State!");
            state.playerPicking.enabled = false;
            taskDone = new bool[5]; 
            state.aiPos = state.listOfAI.items[2].GetComponent<RobotStateManager>().targets[6].transform;
        }

        public override void OnExitState(SceneStateManager state)
        {

        }

        public override void OnUpdateState(SceneStateManager state)
        {
            CheckIfPlayerIsInThePlaceHolder(state);
            ActivateAI(state);
        }

        private void CheckIfPlayerIsInThePlaceHolder(SceneStateManager state)
        {
            if (Vector3.Distance(state.playerTransform.position, state.room2PlayerPos.position) < 1.5f && !playerInPosition) // if player has positioned to its place holder
            {
                if (!rotated)
                {
                    rotated = true;
                    foreach (var ai in state.listOfAI.items)
                    {
                        ai.transform.rotation = Quaternion.identity;
                    }
                }
                Debug.Log("Player is In position");
                PlayerInput.Instance.DisablePlayerMovement(true);
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
                    state.playerPicking.enabled = true;
                    PlayerInput.Instance.DisablePlayerMovement(false);
                    taskDone[1] = true;
                }
                else if (GetNumberOfActiveSockets(state) == 3 && !taskDone[2] && !enableAI && Vector3.Distance(state.playerTransform.position, state.room2PlayerPos.position) < 1.5f)
                {
                    PlayerInput.Instance.DisablePlayerMovement(true);
                    state.StartCoroutine(StartAITask(state, 2));
                    enableAI = true;
                    taskDone[2] = true;
                }
                if (GetNumberOfActiveSockets(state) == 4 && !taskDone[3] && Vector3.Distance(state.listOfAI.items[2].transform.position, state.aiPos.position) < 3f)
                {
                    PlayerInput.Instance.DisablePlayerMovement(false);
                    state.room2Door.EnableDoor = true;
                    for (int i = 0; i < state.listOfAI.Count; i++)
                    {
                        RobotStateManager ai = state.listOfAI.items[i].GetComponent<RobotStateManager>();
                        ai.targetIndex++;
                        ai.moveOnly = true;
                        ai.currentTarget = ai.targets[ai.targetIndex];
                        state.StartCoroutine(ProceedToNextRoom(state, ai));
                    }
                    taskDone[3] = true;
                }
            }

            Debug.Log(Vector3.Distance(state.listOfAI.items[2].transform.position, state.aiPos.position));
        }

        IEnumerator StartAITask(SceneStateManager state, int aiIndex) //Starting AI to do its task
        {
            yield return new WaitForSeconds(3f);
            state.listOfAI.items[aiIndex].GetComponent<RobotStateManager>().moveOnly = false;
            state.listOfAI.items[aiIndex].GetComponent<RobotStateManager>().activateAI = true;
            aiActive = true;
            enableAI = false;
        }

        public int GetNumberOfActiveSockets(SceneStateManager state)
        {
            int activeSockets = 0;

            foreach (var btn in state.room2Buttons.Items)
            {
                if (btn.GetComponent<BatterySocket>().socketIsActive)
                {
                    activeSockets++;
                }
            }
            return activeSockets;
        }

        IEnumerator ProceedToNextRoom(SceneStateManager state, RobotStateManager ai)
        {
            yield return new WaitForSeconds(2f);
            ai.SwitchState(ai.moveState);
            state.SwitchState(state.room3State);
        }

        void DisableAIMoveOnly(SceneStateManager state)
        {
            foreach (GameObject ai in state.listOfAI.Items)
            {
                if (ai.GetComponent<RobotStateManager>().moveOnly)
                {
                    ai.GetComponent<RobotStateManager>().moveOnly = false;
                }
            }
        }
    }
}