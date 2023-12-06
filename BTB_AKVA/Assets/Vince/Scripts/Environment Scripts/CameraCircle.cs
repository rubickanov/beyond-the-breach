using AKVA.Vince.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCircle : MonoBehaviour
{
    [SerializeField] BoolReference playerDead;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerDead.value = true;
        }
    }
}
