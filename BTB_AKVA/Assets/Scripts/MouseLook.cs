using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private  float mouseSensitivity = 100f;

    [SerializeField] private Transform playerBody;

    private float upRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        upRotation -= mouseY;
        upRotation = Mathf.Clamp(upRotation, -90f, 90f);
        
        
        transform.localRotation = Quaternion.Euler(upRotation, 0f, 0f);
        
        playerBody.Rotate(Vector3.up * mouseX);
           
    }
}
