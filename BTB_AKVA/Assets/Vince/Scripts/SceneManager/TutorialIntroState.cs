using AKVA.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class TutorialIntroState : SceneState
    {
        GameObject playerHUDSprite;
        GameObject playerHUDNoSprite;
        Image blackImage;
        TextMeshProUGUI initializeTxt;
        float targetValue = 1f;
        float blackBGDelayTime;
        float currentAlphaValue;
        float currentBGAlphaVal;
        int dotNum;
        bool initTxtAnim = true;
        bool movementTxtEnabled;
        SceneStateManager state;
        bool successSFxPlayed;
        bool labSpeakerEnabled;
        
        public override void OnEnterState(SceneStateManager state)
        {
            initializeTxt = state.initializeTxt;
            playerHUDSprite = state.PlayerHUDSprite;
            playerHUDNoSprite = state.PlayerHUDWithoutAnim;
            blackBGDelayTime = state.bgImageFadeOutTimeDelay;
            blackImage = state.blackBG;
            AdjustInitTxtAlpha(0);
            AdjustBlackBGAlpha(1);
            state.StartCoroutine(InitTxtAnim());
            state.StartCoroutine(ActivatePlayerHUD(state));
            this.state = state;
        }

        public override void OnExitState(SceneStateManager state)
        {
        }

        public override void OnUpdateState(SceneStateManager state)
        {
            BeginShowingInitializeTxt(state);
            state.StartCoroutine(FadeOutBlackScreen(state));
        }

        private void BeginShowingInitializeTxt(SceneStateManager state)
        {
            if (currentAlphaValue < targetValue)
            {
                currentAlphaValue = Mathf.Lerp(initializeTxt.color.a, targetValue, state.initTxtFadeInTime * Time.deltaTime);
                AdjustInitTxtAlpha(currentAlphaValue);
            }
        }

        IEnumerator InitTxtAnim()
        {
            while (initTxtAnim)
            {
                yield return new WaitForSeconds(0.5f);

                if (dotNum < 4)
                {
                    state.OnLoad.Invoke();
                    initializeTxt.SetText("INITIALIZING HUD DISPLAY" + new string('.', dotNum));
                }
                else
                {
                    initializeTxt.SetText("INITIALIZING HUD DISPLAY");
                    dotNum = 0;
                }
                dotNum++;
            }
        }

        IEnumerator ActivatePlayerHUD(SceneStateManager state)
        {
            yield return new WaitForSeconds(5f);
            state.OnHUDActivate.Invoke();
            playerHUDSprite.SetActive(true);
            yield return new WaitForSeconds(1f);
            playerHUDNoSprite.SetActive(true);
            playerHUDSprite.SetActive(false);
        }

        IEnumerator FadeOutBlackScreen(SceneStateManager state)
        {
            yield return new WaitForSeconds(blackBGDelayTime);

            if (blackImage.color.a > 0)
            {
                currentBGAlphaVal = Mathf.Lerp(blackImage.color.a, 0, .5f * Time.deltaTime);
                AdjustBlackBGAlpha(currentBGAlphaVal);
            }
            
            if(blackImage.color.a <= 0.3f && !movementTxtEnabled)
            {
                if (!successSFxPlayed)
                {
                    successSFxPlayed = true;
                    state.OnSuccess.Invoke();
                }

                initTxtAnim = false;
                initializeTxt.color = Color.green;
                initializeTxt.SetText("HUD COMPLETE");
                state.tutorialScreen.turnOnTV = true;
                state.StartCoroutine(EnableVoice(2));
            }
        }

        IEnumerator EnableVoice(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            if (!labSpeakerEnabled)
            {
                labSpeakerEnabled = true;
                state.LabSpeaker.Invoke();
                state.StartCoroutine(SetInitializeText(state.movementDelayTime, "MOVEMENT: ENABLED", state));
            }
        }

        IEnumerator SetInitializeText(float delayTime, string txt, SceneStateManager state)
        {
            yield return new WaitForSeconds(delayTime);

            state.neuroLabLogo.SetActive(false);
            state.screenTxt.SetActive(true);
            state.OnSuccess.Invoke();
            initializeTxt.color = Color.green;
            initializeTxt.SetText(txt);
            movementTxtEnabled = true;
            state.SwitchState(state.movementTutorial);
        }



        void AdjustInitTxtAlpha(float alphaValue)
        {
            initializeTxt.color = new Color(initializeTxt.color.r, initializeTxt.color.g, initializeTxt.color.b, alphaValue);
        }

        void AdjustBlackBGAlpha(float alphaValue)
        {
            blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, alphaValue);
        }
    }
}
