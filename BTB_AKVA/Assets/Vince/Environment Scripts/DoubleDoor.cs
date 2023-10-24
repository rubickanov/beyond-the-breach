using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    [SerializeField] Transform leftDoor, rightDoor;
    [SerializeField] bool activated;
    [SerializeField] float doorSpeed = 3f;

    [Header("RayCast")]
    [SerializeField] Transform rayOrigin;
    [SerializeField] LayerMask allowedToOpen;
    [SerializeField] float rayLength = 1f;
    RaycastHit hit;
    float leftDoorInitPos, rightDoorInitPos;
    void Start()
    {
        leftDoorInitPos = leftDoor.position.x;
        rightDoorInitPos = rightDoor.position.x;
    }

    void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        if (activated)
        {
            Ray ray = new Ray(rayOrigin.position, -rayOrigin.forward);
            if (Physics.Raycast(ray, out hit, rayLength, allowedToOpen))
            {
                print("Person Detected");
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }

    private void OpenDoor()
    {
        float leftDoorTargetPos = -3.8f;
        float rightDoorTargetPos = 5f;

        if (leftDoor.position.x > leftDoorTargetPos)
        {
            float newX = Mathf.Lerp(leftDoor.position.x, leftDoorTargetPos, doorSpeed * Time.deltaTime);
            leftDoor.position = new Vector3(newX, leftDoor.position.y, leftDoor.position.z);
        }

        if (rightDoor.position.x < rightDoorTargetPos)
        {
            float newX = Mathf.Lerp(rightDoor.position.x, rightDoorTargetPos, doorSpeed * Time.deltaTime);
            rightDoor.position = new Vector3(newX, rightDoor.position.y, rightDoor.position.z);
        }
    }

    void CloseDoor()
    {
        float leftDoorTargetPos = leftDoorInitPos;
        float rightDoorTargetPos = rightDoorInitPos;

        if (leftDoor.position.x < leftDoorTargetPos)
        {
            float newX = Mathf.Lerp(leftDoor.position.x, leftDoorTargetPos, doorSpeed * Time.deltaTime);
            leftDoor.position = new Vector3(newX, leftDoor.position.y, leftDoor.position.z);
        }

        if (rightDoor.position.x > rightDoorTargetPos)
        {
            float newX = Mathf.Lerp(rightDoor.position.x, rightDoorTargetPos, doorSpeed * Time.deltaTime);
            rightDoor.position = new Vector3(newX, rightDoor.position.y, rightDoor.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + -rayOrigin.forward * rayLength);
    }
}
