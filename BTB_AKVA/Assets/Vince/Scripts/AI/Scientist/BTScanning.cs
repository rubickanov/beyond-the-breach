using AKVA.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTScanning : TreeNode
    {
        private Transform transform;
        private LayerMask bodyScannerLayer;
        private float visionRad;
        ScientistAIAnim anim;

        float maxScanningTime = 20f;
        float currentTime;
        bool animated;
        ScientistBT sciBT;
        public BTScanning(Transform transform, float visionRad, LayerMask bodyScannerLayer)
        {
            this.transform = transform;
            this.bodyScannerLayer = bodyScannerLayer;
            this.visionRad = visionRad;
            anim = transform.GetComponent<ScientistAIAnim>();
            sciBT = transform.GetComponent<ScientistBT>();
        }

        public override NodeState Execute()
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, visionRad, bodyScannerLayer);
            if (colliders.Length > 0)
            {
                if (!animated)
                {
                    transform.GetComponent<ScientistBT>().StartCoroutine(TriggerAnim(1.5f));
                    animated = true;
                }

                if (currentTime < maxScanningTime)
                {
                    currentTime += Time.deltaTime;
                }
                else
                {
                    if (!sciBT.electricityVfx.activeSelf)
                    {
                        transform.GetComponent<ScientistBT>().StartCoroutine(ResetTask(2f));
                        state = NodeState.FAILURE;
                        return state;
                    }
                }
                state = NodeState.RUNNING;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        IEnumerator ResetTask(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            animated = false;
            currentTime = 0;
        }

        IEnumerator TriggerAnim(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            anim.ChangeAnimState(anim.Robot_Idle);
        }
    }
}
