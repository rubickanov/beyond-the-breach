using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class PreMainSceneState : SceneState
    {
        SceneStateManager state;
        public override void OnEnterState(SceneStateManager state)
        {
            this.state = state;
            state.movementTestTxt.gameObject.SetActive(false);
            state.StartCoroutine(ShowSubtitle());
        }


        public override void OnExitState(SceneStateManager state)
        {
        }

        public override void OnUpdateState(SceneStateManager state)
        {
        }

        IEnumerator ShowSubtitle()
        {
            //scientist1
            yield return new WaitForSeconds(state.subtitleDelay);
            state.subtitle.SetActive(true);
            state.Scientist1Sound.Invoke();

            yield return new WaitForSeconds(6f);
            state.Scientist2Sound.Invoke();
            SetSubtitleText("SCIENTIST2:", "Of course. I've double-checked every specimen, sir.");

            yield return new WaitForSeconds(3f);
            //Metal Sound
            state.MetalClangking.Invoke();
            SetSubtitleText("", "[Heavy lifting noises, clanging of metal]");

            yield return new WaitForSeconds(3f);
            SetSubtitleText("", "[Metal Thrown]");
            state.ThrowingMetalSfx.Invoke();

            yield return new WaitForSeconds(3f);
            state.subtitle.SetActive(false);
            UnityEngine.SceneManagement.SceneManager.LoadScene(state.sceneName);
        }

        void SetSubtitleText(string speaker, string dialouge)
        {
            state.speakerTxt.SetText(speaker);
            state.subTxt.SetText(dialouge);
        }
    }
}
