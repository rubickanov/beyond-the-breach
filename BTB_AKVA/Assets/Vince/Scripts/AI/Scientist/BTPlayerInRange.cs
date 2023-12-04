using AKVA.Animations;
using Codice.CM.Common.Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTPlayerInRange : TreeNode
    {
        private Transform transform;
        private Transform visionPos;
        private float visionRad;
        private LayerMask playerLayer;
        float timeToNoticePlayer;
        float currentTime;
        ScientistBT sciBT;
        AlertSlider alertSlider;
        public BTPlayerInRange(Transform transform, Transform visionPos, float visionRad, LayerMask playerLayer, float timeToNoticePlayer)
        {
            this.transform = transform;
            this.visionPos = visionPos;
            this.visionRad = visionRad;
            this.playerLayer = playerLayer;
            this.timeToNoticePlayer = timeToNoticePlayer;
            sciBT = transform.GetComponent<ScientistBT>();
            alertSlider = sciBT.GetComponent<AlertSlider>();
        }

        public override NodeState Execute()
        {
            Collider[] colliders = Physics.OverlapSphere(visionPos.position, visionRad, playerLayer);
            if (colliders.Length > 0 && currentTime < timeToNoticePlayer)
            {
                AlertSlider(true, currentTime);
                currentTime += Time.deltaTime;
            }
            else if(currentTime >= timeToNoticePlayer)
            {
                //parent.parent.SetData("player", colliders[0].transform); //storing data in the root
                state = NodeState.SUCCESS;
                return state;
            }else if(colliders.Length <= 0)
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
