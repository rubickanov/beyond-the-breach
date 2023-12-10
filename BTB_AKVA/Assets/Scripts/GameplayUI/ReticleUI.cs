using System;
using System.Collections;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;


namespace AKVA.GameplayUI
{
    public class ReticleUI : MonoBehaviour
    {
        
        [Header("RETICLE IMAGE HOLDER")] 
        [SerializeField] private Image imageHolder;

        [Header("RETICLE TYPES")] 
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject defaultReticle;
        [SerializeField] private GameObject interactReticle;
        [SerializeField] private GameObject mindControlReticle;
        [SerializeField] private GameObject unMindControlReticle;
        [SerializeField] private GameObject passwordReticle;
        
        [Header("TIMINGS")]
        [SerializeField] private float timeToDisable;
        [SerializeField] private AnimationCurve curveToDisable;
        [SerializeField] private float blendSmoothTime = 0.3f;

        private Animation anim;
        
        public bool IsEnabled;

        private const string BLEND = "Blend";

        private bool isExpanded;
        private float blendValue;

        private void Start()
        {
            defaultReticle.SetActive(true);
            DisableAllReticles();
        }

        private void Update()
        {
            animator.SetFloat(BLEND, blendValue);
        }

        public void SetDefaultUI()
        {
           // imageHolder.sprite = defaultReticle;
           CompressAnimation();
           DisableAllReticles();
        }

        public void SetInteractionUI()
        {
            //imageHolder.sprite = interactReticle;
            ExpandAnimation();
            DisableAllReticles();
            if (blendValue >= 9f)
            {
                interactReticle.SetActive(true);
            }
        }

        public void SetMindControlUI()
        {
            //imageHolder.sprite = mindControlReticle;
            ExpandAnimation();
            DisableAllReticles();
            
            
            if (blendValue >= 9f)
            {
                mindControlReticle.SetActive(true);
            }
        }
        
        public void SetUnMindControlUI()
        {
            //imageHolder.sprite = unMindControlReticle;
            ExpandAnimation();
            DisableAllReticles();
            
            
            if (blendValue >= 9f)
            {
                unMindControlReticle.SetActive(true);
            }
        }
        
        public void SetPasswordReticle()
        {
            //imageHolder.sprite = unMindControlReticle;
            DisableAllReticles();
            passwordReticle.SetActive(true);
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

        public void DisableReticleImmediately()
        {
            float f = 0;
            Color tempColor = imageHolder.color;
            tempColor.a = f;
            imageHolder.color = tempColor;
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

        private void DisableAllReticles()
        {
            interactReticle.SetActive(false);
            mindControlReticle.SetActive(false);
            unMindControlReticle.SetActive(false);
            passwordReticle.SetActive(false);
        }


        private void ExpandAnimation()
        {
                blendValue = Mathf.Lerp(blendValue, 10, blendSmoothTime);
        }

        private void CompressAnimation()
        {
                blendValue = Mathf.Lerp(blendValue, 0, blendSmoothTime);
        }
        
    }
}
