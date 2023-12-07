using AKVA.Animations;
using AKVA.Assets.Vince.Scripts.Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace AKVA.Assets.Vince.Scripts.AI
{
    public class RobotMovement : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 3f;
        [SerializeField] float rotationSpeed = 2f;
        [SerializeField] float delayInterval = 4f;
        [SerializeField] Transform[] waypoints;
        RobotAIAnim anim;
        [SerializeField] public bool activateRobot;
        MoveAI moveAI;
        public int targetIndex;
        bool activated;
        bool indexAdded;
        bool moving;

        private void Start()
        {
            anim = GetComponent<RobotAIAnim>();
            moveAI = GetComponent<MoveAI>();
        }

        private void Update()
        {
            if (activateRobot)
            {
                StartCoroutine(StartDelay());
            }
        }

        void MoveToTheWaypoints()
        {
            if(!moving)
            {
                moving = true;
                indexAdded = false;
                anim.ChangeAnimState(anim.Robot_Run);
                moveAI.FindPath(waypoints[targetIndex]);
            }
            if (Vector3.Distance(transform.position, waypoints[targetIndex].position) <= 2.5f)
            {
                anim.ChangeAnimState(anim.Robot_Scared);
                if (targetIndex >= waypoints.Length - 1) { return; }
                if (!indexAdded)
                {
                    indexAdded = true;
                    StartCoroutine(MoveDelay());
                }
            }
        }

        IEnumerator MoveRobotToTarget(Transform target)
        {
            yield return new WaitForSeconds(delayInterval);
            anim.ChangeAnimState(anim.Robot_Run);
            transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        }

        IEnumerator MoveDelay()
        {
            yield return new WaitForSeconds(delayInterval);
            targetIndex++;
            moving = false;
        }

        IEnumerator StartDelay()
        {
            yield return new WaitForSeconds(3);
            MoveToTheWaypoints();
        }
    }
}
