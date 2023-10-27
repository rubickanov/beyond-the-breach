using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementTutorialState : SceneState
{
    bool[] movementTask;
    public override void OnEnterState(SceneStateManager state)
    {
        state.playerMovement.enabled = true;
        movementTask = new bool[3];
    }
    public override void OnUpdateState(SceneStateManager state)
    {
        CheckPlayerMovement(state);
    }

    public override void OnExitState(SceneStateManager state)
    {
    }

    private void CheckPlayerMovement(SceneStateManager state)
    {
        if (state.playerMovement.enabled)
        {
            if (Input.GetKeyDown(state.controlsSO.forward) && !movementTask[0]) // move forward
            {
                movementTask[0] = true;
                state.StartCoroutine(EnableMovement(state));
            }
            else if (movementTask[0] && Input.GetKeyDown(state.controlsSO.back)) // move back
            {
                movementTask[1] = true;
                state.StartCoroutine(EnableMovement(state));
            }
            else if (movementTask[1] && Input.GetKeyDown(state.controlsSO.right)) // move right
            {
                movementTask[2] = true;
                state.StartCoroutine(EnableMovement(state));
            }
            else if (movementTask[2] && Input.GetKeyDown(state.controlsSO.left)) // move left
            {
                state.StartCoroutine(EnableMovement(state));
                state.roomDoor.EnableDoor = true;
                state.SwitchState(state.room1State);
            }
        }
    }

    IEnumerator EnableMovement(SceneStateManager state)
    {
        // after pressing the key it will disable movement after a sec
        yield return new WaitForSeconds(1f);
        state.playerMovement.enabled = false;

        //Enables Movement again after disabling it
        yield return new WaitForSeconds(state.timeDelayDuringTutorial);
        Debug.Log("Movement Enabled");
        state.playerMovement.enabled = true;
    }
}