using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalSubtitle : MonoBehaviour
{
    TextMeshProUGUI subtitle => GetComponent<TextMeshProUGUI>();
    private void OnEnable()
    {
        subtitle.enabled = true;
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
