using AKVA;
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

    [Header("Colored Button")]
    [SerializeField] bool isColored;
    public Color buttonColor;

    Vector3 btnInitPos;
    public bool btnIsActive;

    public delegate void OnButtonPressed();
    public event OnButtonPressed onButtonPressed;

    public delegate void OnButtonReleased();
    public event OnButtonReleased onButtonReleased;


    private void Start()
    {
        btnInitPos = btn.localPosition;

        if (isColored)
        {
            ChangeBtnColor(buttonColor);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box" || other.tag == "Player")
        {
            if (!isColored)
            {
                ChangeBtnColor(Color.green);
                MoveBtn(new Vector3(btn.localPosition.x, btn.localPosition.y, -0.0006f));
                onButtonPressed?.Invoke();
                btnIsActive = true;
            }
            else if (isColored && other.GetComponent<InteractableBox>() != null)
            {
                InteractableBox box = other.GetComponent<InteractableBox>();
                if (box.boxColor == buttonColor)
                {
                    MoveBtn(new Vector3(btn.localPosition.x, btn.localPosition.y, -0.0006f));
                    onButtonPressed?.Invoke();
                    btnIsActive = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box" || other.tag == "Player")
        {
            if (!isColored)
            {
                ChangeBtnColor(Color.red);
            }
            MoveBtn(btnInitPos);
            onButtonReleased?.Invoke();
            btnIsActive = false;
        }
    }
}
