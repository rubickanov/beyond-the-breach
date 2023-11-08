using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace AKVA.GameplayUI
{
    public class ReticleUI : MonoBehaviour
    {
        
        [Header("RETICLE IMAGE HOLDER")] 
        [SerializeField] private Image imageHolder;

        [Header("RETICLE TYPES")] 
        [SerializeField] private Sprite defaultReticle;
        [SerializeField] private Sprite interactReticle;
        [SerializeField] private Sprite mindControlReticle;
        
        [Header("TIMINGS")]
        [SerializeField] private float timeToDisable;
        [SerializeField] private AnimationCurve curveToDisable;

        public bool IsEnabled;


        public void SetDefaultUI()
        {
            imageHolder.sprite = defaultReticle;
        }

        public void SetInteractionUI()
        {
            imageHolder.sprite = interactReticle;
        }

        public void SetMindControlUI()
        {
            imageHolder.sprite = mindControlReticle;
        }
        
        public void EnableReticle()
        {
            StopCoroutine(DisableReticleCoroutine());
            StartCoroutine(EnableReticleCoroutine());
        }

        public void DisableReticle()
        {
            StartCoroutine(DisableReticleCoroutine());
        }

        private IEnumerator EnableReticleCoroutine()
        {
            float f = 1f;
            Color tempColor = imageHolder.color;
            tempColor.a = f;
            imageHolder.color = tempColor;
            yield return null;
            IsEnabled = true;

            // float fPerTick = 1 / timeToEnable;
            // while (f < 1f)
            // {
            //     f += Time.deltaTime * fPerTick;
            //     float a = curveToEnable.Evaluate(f);
            //     tempColor.a = a;
            //     imageHolder.color = tempColor;
            //     yield return null;
            //
            // }
        }

        private IEnumerator DisableReticleCoroutine()
        {
            IsEnabled = false;
            
            float f = 1f;
            Color tempColor = imageHolder.color;
            
            float fPerTick = 1 / timeToDisable;
            while (f > 0f && !IsEnabled)
            {
                f -= Time.deltaTime * fPerTick;
                float a = curveToDisable.Evaluate(f);
                tempColor.a = f;
                imageHolder.color = tempColor;
                yield return null;
            }
        }
    }
}
