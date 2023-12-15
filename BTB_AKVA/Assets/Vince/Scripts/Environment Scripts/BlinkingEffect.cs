using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class BlinkingEffect : MonoBehaviour
    {
        [SerializeField] float blinkingDelay = 1f;
        [SerializeField] Material blinkMat;
    
        private void Start()
        {
            StartCoroutine(Blinking());
        }

        IEnumerator Blinking()
        {
            while (true)
            {
                yield return new WaitForSeconds(blinkingDelay);
                blinkMat.DisableKeyword("_EMISSION");
                yield return new WaitForSeconds(blinkingDelay);
                blinkMat.EnableKeyword("_EMISSION");
            }
        }
    }
}
