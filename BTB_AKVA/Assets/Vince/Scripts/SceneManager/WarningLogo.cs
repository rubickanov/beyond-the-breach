using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarningLogo : MonoBehaviour
{
    public UnityEvent LogoAppeared;

    public void logoAppeared()
    {
        LogoAppeared?.Invoke();
    }
}
