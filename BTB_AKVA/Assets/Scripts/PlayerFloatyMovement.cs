using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFloatyMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 inputVector;
    [SerializeField] private float speed;

    [SerializeField] private Transform orientation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        inputVector.x = CalculateDirection(KeyCode.A, KeyCode.D);
        inputVector.y = CalculateDirection(KeyCode.LeftControl, KeyCode.LeftShift);
        inputVector.z = CalculateDirection(KeyCode.S, KeyCode.W);

        
    }

    private float CalculateDirection(KeyCode negativeKey, KeyCode positiveKey)
    {
        if (Input.GetKey(negativeKey) && Input.GetKey(positiveKey))
        {
            return 0f;
        }
        
        if (Input.GetKey(negativeKey))
        {
            return -1f;
        }

        if (Input.GetKey(positiveKey))
        {
            return 1f;
        }

        

        return 0f;
    }
}
