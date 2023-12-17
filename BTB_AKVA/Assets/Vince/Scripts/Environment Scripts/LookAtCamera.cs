using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        LookAtCam();
    }

    private void LookAtCam()
    {
        if(Camera.main == null) { gameObject.SetActive(false); return; }
        transform.LookAt(Camera.main.transform);
    }
}
