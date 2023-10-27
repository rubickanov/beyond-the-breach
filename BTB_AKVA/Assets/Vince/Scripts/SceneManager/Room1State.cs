using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Vector3.Distance(state.playerMovement.transform.position, state.playerPlaceHolder.position) < 1.5f && !playerInPosition) // if player has positioned to its place holder
        {
            Debug.Log("Player is In position");
            state.playerMovement.enabled = false;
            state.StartCoroutine(StartAITask(state, 0));
            playerInPosition = true;
        }
    }

    private void ActivateAI(SceneStateManager state)
    {
        if (aiActive)
        {
            if (state.listOfButtons[0].btnIsActive && !buttonActive[0] && !enableAI)
            {
                state.StartCoroutine(StartAITask(state, 1));
                enableAI = true;
                buttonActive[0] = true;
            }
            else if (state.listOfButtons[1].btnIsActive && !buttonActive[1] && !enableAI)
            {
                state.StartCoroutine(StartAITask(state, 2));
                enableAI = true;
                buttonActive[1] = true;
            }
            else if (state.listOfButtons[2].btnIsActive && !buttonActive[3] && !enableAI)
            {
                state.playerMovement.enabled = true;
            }
            if (state.listOfButtons[3].btnIsActive && Vector3.Distance(state.playerMovement.transform.position, state.playerPlaceHolder.position) < 1.5f && !buttonActive[4])
            {
                for (int i = 0; i < state.listOfAI.Length; i++)
                {
                    state.listOfAI[i].moveOnly = true;
                    state.listOfAI[i].currentTarget = state.listOfAI[i].fourthTarget;
                    state.listOfAI[i].SwitchState(state.listOfAI[i].moveState);
                }
                //foreach (AIStateManager ai in state.listOfAI)
                //{
                //    ai.moveOnly = true;
                //    ai.currentState = ai.moveState;
                //}
                buttonActive[4] = true;
            }
        }
    }


    IEnumerator StartAITask(SceneStateManager state, int aiIndex) //Starting AI to do its task
    {
        yield return new WaitForSeconds(3f);
        state.listOfAI[aiIndex].activateAI = true;
        aiActive = true;
        enableAI = false;
    }
}