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
        [SerializeField] float startDelay = 3;
        [SerializeField] float delayInterval = 4f;
        [SerializeField] float stoppingDistance = 1f;
        [SerializeField] Transform[] waypoints;
        RobotAIAnim anim;
        [SerializeField] public bool activateRobot;
        public int targetIndex;
        public bool moveToNextLocation;
        bool sawDoorOpened;


        private void Start()
        {
            anim = GetComponent<RobotAIAnim>();
        }

        private void Update()
        {
            if (activateRobot)
            {
                StartCoroutine(StartRobotDelay());
            }
        }

        void MoveToTheWaypoints()
        {
            if (Vector3.Distance(transform.localPosition, waypoints[targetIndex].localPosition) >= stoppingDistance && moveToNextLocation)
            {
                anim.ChangeAnimState(anim.Robot_Run);
                transform.position = Vector3.MoveTowards(transform.position, waypoints[targetIndex].position, movementSpeed * Time.deltaTime);

                Vector3 directionToTarget = (waypoints[targetIndex].position - transform.position).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

                transform.rotation = targetRotation;
            }
            else if(Vector3.Distance(transform.localPosition, waypoints[targetIndex].localPosition) <= stoppingDistance && moveToNextLocation)
            {
                anim.ChangeAnimState(anim.Robot_Scared);
                if (targetIndex >= waypoints.Length - 1) { return; }
                targetIndex++;
                moveToNextLocation = false;
            }
        }

        IEnumerator StartRobotDelay()
        {
            if (!sawDoorOpened)
            {
                Quaternion targetRot = Quaternion.LookRotation(Vector3.right);
                transform.rotation = targetRot;
                sawDoorOpened = true;
            }

            yield return new WaitForSeconds(3);
            MoveToTheWaypoints();
        }
    }
}
