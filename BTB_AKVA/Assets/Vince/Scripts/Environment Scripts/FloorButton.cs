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
    bool pressed;
    bool unpressed;

    public delegate void OnButtonPressed();
    public event OnButtonPressed onButtonPressed;

    public delegate void OnButtonReleased();
    public event OnButtonReleased onButtonReleased;


    private void Start()
    {
        btnInitPos = btn.localPosition;
    }

    private void Update()
    {
        DetectObject();
    }

    private void DetectObject()
    {
        if (Physics.CheckBox(transform.position, Vector3.one/2, Quaternion.identity, allowedObjects))
        {
            ChangeBtnColor(Color.green);
            if (!pressed)
            {
                pressed = true;
                onButtonPressed?.Invoke();
            }
            MoveBtn(new Vector3(btn.localPosition.x, btn.localPosition.y, -0.0006f));
            unpressed = false;
        }
        else
        {
            if (!unpressed)
            {
                pressed = false;
                onButtonReleased?.Invoke();
                unpressed = true;
            }
            ChangeBtnColor(Color.red);
            MoveBtn(btnInitPos);
        }
    }

    void ChangeBtnColor(Color color)
    {
        btnMat.material.color = color;
    }

    void MoveBtn(Vector3 pos)
    {
        btn.localPosition = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 0.5f, 1));
    }
}
