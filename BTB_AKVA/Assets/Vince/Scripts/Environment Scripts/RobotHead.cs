using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class RobotHead : MonoBehaviour
    {
        [SerializeField] float blinkingDelay = 1f;
        [SerializeField] Material robotHeadMat;
    
        private void Start()
        {
            StartCoroutine(BlinkingRobotHead());
        }

        IEnumerator BlinkingRobotHead()
        {
            while (true)
            {
                yield return new WaitForSeconds(blinkingDelay);
                robotHeadMat.DisableKeyword("_EMISSION");
                yield return new WaitForSeconds(blinkingDelay);
                robotHeadMat.EnableKeyword("_EMISSION");
            }
        }
    }
}
