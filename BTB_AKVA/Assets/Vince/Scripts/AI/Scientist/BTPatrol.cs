using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Astar;
using Codice.Client.Common.TreeGrouper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.AI
{
    public class BTPatrol : TreeNode
    {
        Transform currentTransform;
        Transform[] wayPoints;
        MoveAI moveAI;
        int targetIndex = 0;
        bool aiMoved;
        ScientistAIAnim anim;
        public BTPatrol(Transform currentTransform, Transform[] wayPoints)
        {
            anim = currentTransform.GetComponent<ScientistAIAnim>();
            moveAI = currentTransform.GetComponent<MoveAI>();
            this.currentTransform = currentTransform;
            this.wayPoints = wayPoints;
        }
        public override NodeState Execute()
        {
            if (wayPoints.Length > 0)
            {
                if (!aiMoved)
                {
                    anim.ChangeAnimState(anim.Robot_Walk);
                    moveAI.FindPath(wayPoints[targetIndex]);
                    aiMoved = true;
                }
                if (targetIndex == wayPoints.Length - 1)
                {
                    currentTransform.position = wayPoints[targetIndex].position;
                    targetIndex = 0;
                    aiMoved = false;
                }
                else if (Vector3.Distance(currentTransform.position, wayPoints[targetIndex].position) <= 2.5f)
                {
                    targetIndex++;
                    aiMoved = false;
                }
                state = NodeState.RUNNING;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}

