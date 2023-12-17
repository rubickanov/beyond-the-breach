using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private Transform orientation;

        private void Update()
        {
            transform.forward = orientation.forward;
        }
    }
}
