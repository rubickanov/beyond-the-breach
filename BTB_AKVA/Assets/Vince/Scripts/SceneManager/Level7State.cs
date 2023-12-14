using AKVA.Assets.Vince.Scripts.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.SceneManager
{
    public class Level7State : LevelState
    {
        bool [] robotsTriggered;
        public override void OnEnterState(LevelManager state)
        {
            robotsTriggered = new bool[3];
            foreach(GameObject scientist in state.scientistsLevel7)
            {
                scientist.SetActive(true);
            }
        }

        public override void OnUpdateState(LevelManager state)
        {
            CheckIfPlayerHasReachedTriggerPos(state);
        }

        private void CheckIfPlayerHasReachedTriggerPos(LevelManager state)
        {
            if(Vector3.Distance(state.player.transform.position, state.robotQueueTriggerPoint.position) < state.distanceToTriggerRobots)
            {
                TriggerRunningRobotsTowardTheExit(state, 0);
            }

            if (Vector3.Distance(state.player.transform.position, state.robotQueueTriggerPoint2.position) < 1.5)
            {
                TriggerRunningRobotsTowardTheExit(state, 1);
            }
        }

        void TriggerRunningRobotsTowardTheExit(LevelManager state, int queueIndex)
        {
            if (!robotsTriggered[queueIndex])
            {
                robotsTriggered[queueIndex] = true;

                foreach(RobotMovement robots in state.level7Robots)
                {
                    robots.moveToNextLocation = true;
                }
            }
        }
    }
}
