using AstarAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAIWithAStar : MonoBehaviour
{
    [SerializeField] Astar astar;
    [SerializeField] public Transform targetPos;
    public float movementSpeed = 3f;
    List<Vector3> path;
    public bool hasMoved;
    int currentPathIndex;
    bool onTargetPos;
    void Awake()
    {
        path = new List<Vector3>();
    }

    void Update()
    {
        GoThroughWayPoints();
        Move();
    }

    public void MoveAI(Transform target)
    {
        targetPos = target;
        hasMoved = true;
    }

    void Move()
    {
        if (hasMoved && targetPos != null)
        {
            if (astar.FindPath(transform.position, targetPos.position))
            {
                currentPathIndex = 0;
                onTargetPos = false;
                path.Clear();
                foreach (Node node in astar.path)
                {
                    path.Add(node.WorldPosition);
                }
            }
            hasMoved = false;
        }
    }

    private void GoThroughWayPoints()
    {
        if (path.Count > 0 && currentPathIndex < path.Count)
        {
            //Vector3 targetPosition = path[currentPathIndex];
            Vector3 targetPosition = new Vector3(path[currentPathIndex].x, transform.position.y, path[currentPathIndex].z);

            Vector3 direction = (targetPosition - transform.position).normalized;
            //transform.position += direction * Time.deltaTime * movementSpeed;
            transform.position += direction * Time.deltaTime * movementSpeed;
            LookAtTarget();

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
            }

        }

        else if (path.Count > 0 && currentPathIndex >= path.Count && !onTargetPos)
        {
            if (Vector3.Distance(transform.position, targetPos.position) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos.position, 5 * Time.deltaTime);
            }
            else
            {
                onTargetPos = true;
            }
        }
    }

    private void LookAtTarget()
    {
        Vector3 directionToTarget = (path[currentPathIndex] - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

        transform.rotation = targetRotation;
    }

    private void OnDrawGizmos()
    {
        //if (path.Count <= 0) { return; }
        //Gizmos.color = Color.green;

        //foreach (Vector3 wayPoint in path)
        //{
        //    Gizmos.DrawWireSphere(wayPoint, .1f);
        //}
    }
}
