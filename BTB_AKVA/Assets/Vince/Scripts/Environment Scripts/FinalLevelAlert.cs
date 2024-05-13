using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalLevelAlert : MonoBehaviour
{
    [SerializeField] float startDelay = 3f;
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioClip securityAlertSFX;
    [SerializeField] Renderer[] renderers;
    [SerializeField] Material materialToChange;
    [SerializeField] UnityEvent RedLight;
    [SerializeField] UnityEvent SecurityPA;

    private void Awake()
    {
        materialToChange.color = Color.white;
    }

    public void TriggerAlert()
    {
        StartCoroutine(TriggerAlarm());
    }

    IEnumerator TriggerAlarm()
    {
        yield return new WaitForSeconds(startDelay);

        foreach (Renderer renderer in renderers)
        {
            renderer.material = materialToChange;
        }

        StartCoroutine(LightLoop());
    }

    public void EnableSecurityAlertSFX()
    {
        StartCoroutine(AlertDelay());
    }

    IEnumerator AlertDelay()
    {
        yield return new WaitForSeconds(4f);

        while (true)
        {
            yield return new WaitForSeconds(5);
            //SubtitleManager.Instance.PlayPublicAnnoucememnt("PA SYSTEM:", "Security Alert: Control room has been breached. All robots have escaped.", 5f);
            SecurityPA.Invoke();
            yield return new WaitForSeconds(6);
            //SubtitleManager.Instance.PlayPublicAnnoucememnt("PA SYSTEM:", "Security Alert: Control room has been breached. All robots have escaped.", 5f);
            SecurityPA.Invoke();
        }
        //sfxAudioSource.clip = securityAlertSFX;
        //sfxAudioSource.loop = true;
        //sfxAudioSource.volume = 0.2f;
        //sfxAudioSource.Play();
    }

    IEnumerator LightLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            RedLight.Invoke();
            materialToChange.color = Color.red;

            yield return new WaitForSeconds(1);
            materialToChange.color = Color.white;
        }
    }

    private void OnDisable()
    {
        materialToChange.color = Color.white;
    }
}
