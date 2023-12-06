using AKVA.Vince.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class ElectricRadiation : MonoBehaviour
    {
        [SerializeField] BoolReference switchEnable;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" && switchEnable.value)
            {
                Debug.Log("Game Over");
            }
        }

    }
}
