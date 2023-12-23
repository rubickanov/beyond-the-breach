using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTPlayerInRange : TreeNode
    {
        private Transform transform;
        private Transform visionPos;
        private Transform eyespos;
        private float visionRad;
        private LayerMask playerLayer;
        private LayerMask obstructionMask;
        float timeToNoticePlayer;
        float currentTime;
        float angle = 100;
        ScientistBT sciBT;
        AlertSlider alertSlider;
        Transform target;
        public BTPlayerInRange(Transform transform, Transform visionPos, float visionRad, float timeToNoticePlayer, Transform eyespos, LayerMask playerLayer, LayerMask obstructionMask)
        {
            this.transform = transform;
            this.visionPos = visionPos;
            this.visionRad = visionRad;
            this.playerLayer = playerLayer;
            this.timeToNoticePlayer = timeToNoticePlayer;
            sciBT = transform.GetComponent<ScientistBT>();
            alertSlider = sciBT.GetComponent<AlertSlider>();
            this.obstructionMask = obstructionMask;
            this.eyespos = eyespos;
        }

        public override NodeState Execute()
        {
            Collider[] colliders = Physics.OverlapSphere(visionPos.position, visionRad, playerLayer);
            if (colliders.Length > 0)
            {
                target = colliders[0].transform;
                Vector3 directionToTarget = (target.position - eyespos.position).normalized;

                if (Vector3.Angle(eyespos.forward, directionToTarget) < sciBT.angleToDetect)
                {
                    float distanceToTarget = Vector3.Distance(eyespos.position, target.position);
                    if (!Physics.Raycast(eyespos.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        if (currentTime < timeToNoticePlayer)
                        {
                            currentTime += Time.deltaTime;
                            AlertSlider(true, currentTime);
                        }
                        else if (currentTime >= timeToNoticePlayer)
                        {
                            state = NodeState.SUCCESS;
                            return state;
                        }
                    }
                    else
                    {
                        currentTime = 0;
                        AlertSlider(false, currentTime);
                    }
                }
                else
                {
                    currentTime = 0;
                    AlertSlider(false, currentTime);
                    state = NodeState.FAILURE;
                    return state;
                }
            }
            else if (colliders.Length <= 0)
            {
                currentTime = 0;
                AlertSlider(false, currentTime);
            }
            state = NodeState.FAILURE;
            return state;
        }

        void AlertSlider(bool enable, float value)
        {
            alertSlider.SetSlider(enable);
            alertSlider.UpdateSliderValue(value);
        }
    }
}
