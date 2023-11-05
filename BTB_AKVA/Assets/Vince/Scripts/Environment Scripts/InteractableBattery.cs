using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class InteractableBattery : MonoBehaviour
    {
        public bool inSocket;
        public bool batteryOnHand;
        public bool isColored;
        public Color boxColor;
        Material material;
        void Start()
        {
            if (isColored)
            {
                material = GetComponent<Renderer>().material;
                material.color = boxColor;
            }
        }
    }
}
