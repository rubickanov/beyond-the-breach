using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Astar
{
    public class MoveAI : MonoBehaviour
    {
        public bool transformLookAt;
        public bool lockYPos = true;
        public float speed = 1;
        Vector3[] path;
        int targetIndex;
        float initY;
        void Start()
        {
            initY = transform.position.y;
        }

        private void Update()
        {
        }

        public void FindPath(Transform target)
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }

        public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                path = newPath;
                targetIndex = 0;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }

        IEnumerator FollowPath()
        {
            Vector3 currentWaypoint = path[0];
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
                float initY = transform.position.y;

                if (lockYPos)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentWaypoint.x, currentWaypoint.y, currentWaypoint.z), speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

                }

                if (transformLookAt)
                {
                    Vector3 directionToTarget = (currentWaypoint - transform.position).normalized;

                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                    targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

                    transform.rotation = targetRotation;
                }

                yield return null;
            }
        }

        public void StopMoving()
        {
            StopAllCoroutines();
        }

        public void OnDrawGizmos()
        {
            if (path != null)
            {
                for (int i = targetIndex; i < path.Length; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one);

                    if (i == targetIndex)
                    {
                        Gizmos.DrawLine(transform.position, path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(path[i - 1], path[i]);
                    }
                }
            }
        }
    }
}