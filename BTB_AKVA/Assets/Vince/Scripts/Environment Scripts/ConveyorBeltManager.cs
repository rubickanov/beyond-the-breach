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

        private void Update()
        {
            MoveObjectsOnTheConveyorBelt();
        }

        private void MoveObjectsOnTheConveyorBelt()
        {
            if (objDetected != null)
            {
                if (conveyorEnabled)
                {
                    objDetected.position += -transform.right * conveyorSpeed * Time.deltaTime;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Scientist")
            {
                Debug.Log("Detected");
                objDetected = collision.gameObject.transform;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Scientist")
            {
                objDetected = null;
            }
        }
    }
}