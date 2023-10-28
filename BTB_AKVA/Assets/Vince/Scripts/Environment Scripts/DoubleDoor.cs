using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    [Header("Connect to button")]
    [SerializeField] FloorButton[] floorButton;
    [SerializeField] float numberOfBtnsActivated;

    [Header("Door Settings")]
    [SerializeField] Transform leftDoor, rightDoor;
    [SerializeField] bool activated;
    [SerializeField] float doorSpeed = 3f;
    [SerializeField] Material doorActivated, doorDeactivated;

    [Header("RayCast")]
    [SerializeField] Transform rayOrigin;
    [SerializeField] LayerMask allowedToOpen;
    [SerializeField] float rayLength = 1f;
    RaycastHit hit;
    float leftDoorInitPos, rightDoorInitPos;

    #region Properties
    public bool EnableDoor { set => activated = value; }
    #endregion

    private void OnEnable()
    {
        if (floorButton.Length > 0 || floorButton != null)
        {
            foreach (FloorButton button in floorButton)
            {
                button.onButtonPressed += ActivateDoor;
                button.onButtonReleased += DeactivateDoor;
            }
        }
    }

    private void OnDisable()
    {
        if (floorButton.Length > 0)
        {
            foreach (FloorButton button in floorButton)
            {
                button.onButtonPressed -= ActivateDoor;
                button.onButtonReleased -= ActivateDoor;
            }
        }
    }

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
            ChangeDoorColor(doorActivated);
            Ray ray = new Ray(rayOrigin.position, -rayOrigin.forward);
            if (Physics.Raycast(ray, out hit, rayLength, allowedToOpen))
            {
                OpenDoor();
            } 
            else
            {
                CloseDoor();
            }
        }
        else
        {
            ChangeDoorColor(doorDeactivated);
        }

        if (numberOfBtnsActivated < 0)
        {
            numberOfBtnsActivated = 0;
        }
    }

    void ChangeDoorColor(Material material)
    {
        leftDoor.GetComponent<Renderer>().material = material;
        rightDoor.GetComponent<Renderer>().material = material;
    }

    void ActivateDoor()
    {
        numberOfBtnsActivated++;
        if (numberOfBtnsActivated >= floorButton.Length)
        {
            activated = true;
        }
        else
        {
            activated = false;
        }
    }

    void DeactivateDoor()
    {
        numberOfBtnsActivated--;
        activated = false;
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