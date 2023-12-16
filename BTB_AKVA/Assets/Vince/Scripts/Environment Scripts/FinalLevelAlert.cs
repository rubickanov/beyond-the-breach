using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelAlert : MonoBehaviour
{
    [SerializeField] float startDelay = 3f;
    [SerializeField] Renderer[] renderers;
    [SerializeField] Material materialToChange;

    public void TriggerAlert()
    {
        StartCoroutine(TriggerAlarm());
    }

    IEnumerator TriggerAlarm()
    {
        yield return new WaitForSeconds(startDelay);

        foreach(Renderer renderer in renderers)
        {
            renderer.material = materialToChange;
        }

        StartCoroutine(LightLoop());
    }

    IEnumerator LightLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            materialToChange.color = Color.red;

            yield return new WaitForSeconds(1);
            materialToChange.color = Color.white;

        }
    }
}
