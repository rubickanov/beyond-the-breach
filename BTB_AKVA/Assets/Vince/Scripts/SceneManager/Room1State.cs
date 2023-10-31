using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AKVA.Player;
using AKVA.Assets.Vince.Scripts.AI;
using AKVA.Assets.Vince.Scripts.Environment;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Room1State : SceneState
    {
        bool playerInPosition;
        bool aiActive; //Initiates the AI task
        bool enableAI; //Initiate Each AI to be activated
        bool[] buttonActive;
        public override void OnEnterState(SceneStateManager state)
        {
            Debug.Log("Room 1 State");
            buttonActive = new bool[5];
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
            if (Vector3.Distance(state.playerTransform.position, state.playerPlaceHolder.position) < 1.5f && !playerInPosition) // if player has positioned to its place holder
            {
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
                if (state.room1Buttons.items[0].GetComponent<FloorButton>().btnIsActive && !buttonActive[0] && !enableAI)
                {
                    state.StartCoroutine(StartAITask(state, 1));
                    enableAI = true;
                    buttonActive[0] = true;
                }
                else if (state.room1Buttons.items[1].GetComponent<FloorButton>().btnIsActive && !buttonActive[1] && !enableAI)
                {
                    state.StartCoroutine(StartAITask(state, 2));
                    enableAI = true;
                    buttonActive[1] = true;
                }
                else if (state.room1Buttons.items[2].GetComponent<FloorButton>().btnIsActive && !buttonActive[3] && !enableAI)
                {
                    PlayerInput.Instance.DisablePlayerMovement(false);
                }
                if (state.room1Buttons.items[3].GetComponent<FloorButton>().btnIsActive && Vector3.Distance(state.playerTransform.position, state.playerPlaceHolder.position) < 1.5f && !buttonActive[4])
                {
                    for (int i = 0; i < state.listOfAI.Count; i++)
                    {
                        AIStateManager ai = state.listOfAI.items[i].GetComponent<AIStateManager>();
                        ai.moveOnly = true;
                        ai.currentTarget = ai.fourthTarget;
                        state.StartCoroutine(ProceedToNextRoom(state,ai));
                    }
                    buttonActive[4] = true;
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

        IEnumerator ProceedToNextRoom(SceneStateManager state, AIStateManager ai)
        {
            yield return new WaitForSeconds(2f);
            ai.SwitchState(ai.moveState);
            state.SwitchState(state.room2State);
        }
    }
}