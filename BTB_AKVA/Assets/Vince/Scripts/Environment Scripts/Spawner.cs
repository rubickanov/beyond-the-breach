using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Environment
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] Transform spawnPos;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Scientist")
            {
                other.gameObject.transform.position = spawnPos.position;
            }
        }
    }
}