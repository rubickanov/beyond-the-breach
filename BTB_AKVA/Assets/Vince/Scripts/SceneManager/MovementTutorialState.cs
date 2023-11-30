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
            PlayerInput.Instance.EnablePlayerMovement();
            movementTask = new bool[3];
            state.tutorialScreen.turnOnTV = true;
            state.tutorialScreen.SetKeyLettersAndInsruction("W", "To Move Forward");
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
                    state.tutorialScreen.SetKeyLettersAndInsruction("S", "To Move Backwards");
                    state.StartCoroutine(EnableMovement(state));
                }
                else if (movementTask[0] && Input.GetKeyDown(PlayerInput.Instance.Controls.backwards)) // move back
                {
                    movementTask[1] = true;
                    state.tutorialScreen.SetKeyLettersAndInsruction("D", "To Move Right");
                    state.StartCoroutine(EnableMovement(state));
                }
                else if (movementTask[1] && Input.GetKeyDown(PlayerInput.Instance.Controls.right)) // move right
                {
                    movementTask[2] = true;
                    state.tutorialScreen.SetKeyLettersAndInsruction("A", "To Move Left");
                    state.StartCoroutine(EnableMovement(state));
                }
                else if (movementTask[2] && Input.GetKeyDown(PlayerInput.Instance.Controls.left)) // move left
                {
                    state.tutorialScreen.ProceedToNextRoomText();
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
            PlayerInput.Instance.DisablePlayerMovement();

            //Enables Movement again after disabling it
            yield return new WaitForSeconds(state.timeDelayDuringTutorial);
            Debug.Log("Movement Enabled");
            PlayerInput.Instance.EnablePlayerMovement();
        }
    }
}