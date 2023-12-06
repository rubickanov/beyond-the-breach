using AKVA.Vince.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] BoolReference playerDead;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerDead.value = true;
        }
    }
}
