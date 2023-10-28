using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorButton : MonoBehaviour
{
    [SerializeField] Renderer btnMat;
    [SerializeField] LayerMask allowedObjects;
    [SerializeField] Transform btn;
    Vector3 btnInitPos;
    public bool btnIsActive;

    public delegate void OnButtonPressed();
    public event OnButtonPressed onButtonPressed;

    public delegate void OnButtonReleased();
    public event OnButtonReleased onButtonReleased;


    private void Start()
    {
        btnInitPos = btn.localPosition;
    }

    void ChangeBtnColor(Color color)
    {
        btnMat.material.color = color;
    }

    void MoveBtn(Vector3 pos)
    {
        btn.localPosition = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box" || other.tag == "Player")
        {
            ChangeBtnColor(Color.green);
            btnIsActive = true;
            onButtonPressed?.Invoke();
            MoveBtn(new Vector3(btn.localPosition.x, btn.localPosition.y, -0.0006f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box" || other.tag == "Player")
        {
            onButtonReleased?.Invoke();
            btnIsActive = false;
            ChangeBtnColor(Color.red);
            MoveBtn(btnInitPos);
        }
    }
}
