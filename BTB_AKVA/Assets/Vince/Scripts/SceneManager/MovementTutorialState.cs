using System.Collections;
using UnityEngine;
using AKVA.Player;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class MovementTutorialState : SceneState
    {
        bool[] movementTask;
        public override void OnEnterState(SceneStateManager state)
        {
            PlayerInput.Instance.DisablePlayerMovement(false);
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
            if (PlayerInput.Instance.GetPlayerMovement)
            {
                if (Input.GetKeyDown(PlayerInput.Instance.Controls.forward) && !movementTask[0]) // move forward
                {
                    movementTask[0] = true;
                    state.StartCoroutine(EnableMovement(state));
                }
                else if (movementTask[0] && Input.GetKeyDown(PlayerInput.Instance.Controls.backwards)) // move back
                {
                    movementTask[1] = true;
                    state.StartCoroutine(EnableMovement(state));
                }
                else if (movementTask[1] && Input.GetKeyDown(PlayerInput.Instance.Controls.right)) // move right
                {
                    movementTask[2] = true;
                    state.StartCoroutine(EnableMovement(state));
                }
                else if (movementTask[2] && Input.GetKeyDown(PlayerInput.Instance.Controls.left)) // move left
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
            PlayerInput.Instance.DisablePlayerMovement(true);

            //Enables Movement again after disabling it
            yield return new WaitForSeconds(state.timeDelayDuringTutorial);
            Debug.Log("Movement Enabled");
            PlayerInput.Instance.DisablePlayerMovement(false);
        }
    }
}