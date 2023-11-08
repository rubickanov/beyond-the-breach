using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Interaction
{
    public class PassDeviceChecker : MonoBehaviour
    {
        [SerializeField] private Transform cam;
        [SerializeField] private float distanceToInteract;
        [SerializeField] private LayerMask buttonLayer;
        
        public bool IsActive;

        private void Update()
        {
            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, distanceToInteract, buttonLayer))
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
        }
    }
}
