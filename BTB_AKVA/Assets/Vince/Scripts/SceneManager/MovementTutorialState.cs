using System.Collections;
using UnityEngine;
using AKVA.Player;
using AKVA.Assets.Vince.Scripts.Environment;
using TMPro;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class MovementTutorialState : SceneState
    {
        bool[] movementTask;
        bool txtAnim = true;
        int dotNum = 0;
        TextMeshProUGUI movementCheckTxt;
        bool started;

        public override void OnEnterState(SceneStateManager state)
        {
            movementTask = new bool[3];
            movementCheckTxt = state.movementTestTxt;
            movementCheckTxt.gameObject.SetActive(true);
            if (!started)
            {
                movementCheckTxt.color = state.hudColor;
                started = true;
                state.StartCoroutine(AnimTxt());
            }

            state.tutorialScreen.turnOnTV = true;
            state.tutorialScreen.SetKeyLettersAndInsruction("[W]", "TO MOVE FORWARD");
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
                    state.tutorialScreen.SetKeyLettersAndInsruction("[S]", "TO MOVE BACKWARDS");
                    state.StartCoroutine(EnableMovement(state));
                }
                else if (movementTask[0] && Input.GetKeyDown(PlayerInput.Instance.Controls.backwards)) // move back
                {
                    movementTask[1] = true;
                    state.tutorialScreen.SetKeyLettersAndInsruction("[D]", "TO MOVE RIGHT");
                    state.StartCoroutine(EnableMovement(state));
                }
                else if (movementTask[1] && Input.GetKeyDown(PlayerInput.Instance.Controls.right)) // move right
                {
                    movementTask[2] = true;
                    state.tutorialScreen.SetKeyLettersAndInsruction("[A]", "TO MOVE LEFT");
                    state.StartCoroutine(EnableMovement(state));
                }
                else if (movementTask[2] && Input.GetKeyDown(PlayerInput.Instance.Controls.left)) // move left
                {
                    state.tutorialScreen.ProceedToNextRoomText();
                    PlayerInput.Instance.EnablePlayerMovement();
                 
                    foreach (DoubleDoor door in state.roomDoor)
                    {
                        door.EnableDoor = true;
                    }

                    txtAnim = false;
                    state.StopCoroutine(AnimTxt());
                    state.SwitchState(state.room1State);
                }
            }
        }

        IEnumerator AnimTxt()
        {
            while (txtAnim)
            {
                yield return new WaitForSeconds(.5f);

                if (dotNum < 4)
                {
                    movementCheckTxt.SetText("CHECKING MOVEMENT SYSTEM" + new string('.', dotNum));
                }
                else
                {
                    movementCheckTxt.SetText("CHECKING MOVEMENT SYSTEM");
                    dotNum = 0;
                }
                dotNum++;
            }
            movementCheckTxt.SetText("MOVEMENT SYSTEM SUCCESS");
        }

        IEnumerator EnableMovement(SceneStateManager state)
        {
            // after pressing the key it will disable movement after a sec
            yield return new WaitForSeconds(1f);
            PlayerInput.Instance.DisablePlayerMovement();

            //Enables Movement again after disabling it
            yield return new WaitForSeconds(state.timeDelayDuringTutorial);
            PlayerInput.Instance.EnablePlayerMovement();
        }
    }
}