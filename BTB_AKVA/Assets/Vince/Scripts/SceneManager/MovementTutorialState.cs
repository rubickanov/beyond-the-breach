using System.Collections;
using UnityEngine;
using AKVA.Player;
using AKVA.Assets.Vince.Scripts.Environment;
using TMPro;
using System;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class MovementTutorialState : SceneState
    {
        bool[] movementTask;
        bool txtAnim = true;
        int dotNum = 0;
        TextMeshProUGUI movementCheckTxt;
        bool started;
        bool movementSpeechEnabled;
        SceneStateManager state;

        public override void OnEnterState(SceneStateManager state)
        {
            this.state = state;
            movementTask = new bool[3];
            movementCheckTxt = state.movementTestTxt;
            movementCheckTxt.gameObject.SetActive(true);
            if (!started)
            {
                movementCheckTxt.color = state.hudColor;
                started = true;
                state.StartCoroutine(AnimTxt());
            }
            state.StartCoroutine(EnableMovement(2));
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
                if (Input.GetKeyDown(PlayerInput.Instance.Controls.forward) && !movementTask[0] && !movementTask[1])// move forward
                {
                    movementTask[0] = true;
                    state.tutorialScreen.SetKeyLettersAndInsruction("[S]", "TO MOVE BACKWARDS");
                    state.StartCoroutine(EnableMovement(0f));
                }
                else if (movementTask[0] && Input.GetKeyDown(PlayerInput.Instance.Controls.backwards)) // move back
                {
                    movementTask[1] = true;
                    state.tutorialScreen.SetKeyLettersAndInsruction("[D]", "TO MOVE RIGHT");
                    state.StartCoroutine(EnableMovement(0f));
                }
                else if (movementTask[1] && Input.GetKeyDown(PlayerInput.Instance.Controls.right)) // move right
                {
                    movementTask[2] = true;
                    state.tutorialScreen.SetKeyLettersAndInsruction("[A]", "TO MOVE LEFT");
                    state.StartCoroutine(EnableMovement(0f));
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
                    state.OnLoad.Invoke();
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
            EnableMovementSpeech();
        }

        IEnumerator EnableMovement(float time)
        {
            //Enables Movement again after disabling it
            yield return new WaitForSeconds(time);
            PlayerInput.Instance.EnablePlayerInput();
        }

        void EnableMovementSpeech()
        {
            if (!movementSpeechEnabled)
            {
                movementSpeechEnabled = true;
                state.MovementSuccess.Invoke();
            }
        }
    }
}