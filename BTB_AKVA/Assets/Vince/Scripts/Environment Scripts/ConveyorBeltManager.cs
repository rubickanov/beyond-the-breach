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
        [SerializeField] float objectConveyorSpeed = 0.7f;
        [SerializeField] private float objectOnConveyorSpeed = 90.0f; // IF YOU CHANGE CONVEYOR SPEED - YOU ALSO NEED TO CHANGE THIS VARIABLE TO MATCH SPEED OF CONVEYOR AND FORCE ON OBJECT 
        Transform objDetected;
        [SerializeField] List<Transform> junks;
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
            MoveConveyorJunks();
        }

        void MoveConveyorJunks()
        {
            if (junks.Count > 0)
            {
                foreach (Transform t in junks)
                {
                    t.position += -transform.right * objectConveyorSpeed * Time.deltaTime;
                }
            }
        }

        private void MoveObjectsOnTheConveyorBelt()
        {
            if (objDetected != null)
            {
                if (conveyorEnabled)
                {
                    //Rigidbody objectDetectedRigibody = objDetected.GetComponent<Rigidbody>();
                    //objectDetectedRigibody.AddForce(-transform.right * objectOnConveyorSpeed, ForceMode.VelocityChange);
                    objDetected.position += -transform.right * conveyorSpeed * Time.deltaTime;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Scientist"))
            {
                objDetected = collision.gameObject.transform;
            }

            if (collision.gameObject.tag == "Junk" && this.enabled)
            {
                junks.Add(collision.gameObject.transform);
            }
        }


        private void OnCollisionStay(Collision collision)
        {
         
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