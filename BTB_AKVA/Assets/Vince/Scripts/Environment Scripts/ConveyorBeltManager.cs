using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class ConveyorBeltManager : MonoBehaviour
    {
        [SerializeField] Material conveyorMat;
        [SerializeField] bool conveyorEnabled = true;
        [SerializeField] float conveyorSpeed = 0.5f;
        [SerializeField] private float objectOnConveyorSpeed = 90.0f; // IF YOU CHANGE CONVEYOR SPEED - YOU ALSO NEED TO CHANGE THIS VARIABLE TO MATCH SPEED OF CONVEYOR AND FORCE ON OBJECT 
        Transform objDetected;
        private void Start()
        {
            conveyorMat.SetFloat("_AnimSpeed", conveyorSpeed);
        }
        public void EnableConveyorBelt(bool enable)
        {
            conveyorEnabled = enable;
            if (enable)
            {
                conveyorMat.SetFloat("_AnimSpeed", conveyorSpeed);
            }
            else
            {
                conveyorMat.SetFloat("_AnimSpeed", 0f);
            }
        }

        private void FixedUpdate()
        {
            MoveObjectsOnTheConveyorBelt();

            if (objDetected)
            {
                Debug.Log(objDetected.GetComponent<Rigidbody>().velocity);
            }
        }

        private void MoveObjectsOnTheConveyorBelt()
        {
            if (objDetected != null)
            {
                if (conveyorEnabled)
                {
                    Rigidbody objectDetectedRigibody = objDetected.GetComponent<Rigidbody>();
                    objectDetectedRigibody.AddForce(-transform.right * objectOnConveyorSpeed, ForceMode.VelocityChange);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Scientist"))
            {
                //Debug.Log("Detected");
                objDetected = collision.gameObject.transform;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Scientist"))
            {
                objDetected = null;
            }
        }
    }
}