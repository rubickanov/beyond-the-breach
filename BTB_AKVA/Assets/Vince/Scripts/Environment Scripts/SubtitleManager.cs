using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] public AudioClip [] clips;
    [SerializeField] TextMeshProUGUI speakerTxt;
    [SerializeField] TextMeshProUGUI subtitleTxt;

    public static SubtitleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
   
    public void PlayPublicAnnoucememnt(string speaker, string annoucememntTxt, int clipIndex, float txtDuration)
    {
        subtitleTxt.gameObject.SetActive(true);
        speakerTxt.SetText(speaker);
        subtitleTxt.SetText(annoucememntTxt);
        audioSource.PlayOneShot(clips[clipIndex]);
        StartCoroutine(DisableSubtitle(txtDuration));
    }

    public void PlayPublicAnnoucememnt(string speaker, string annoucememntTxt, float txtDuration)
    {
        subtitleTxt.gameObject.SetActive(true);
        speakerTxt.SetText(speaker);
        subtitleTxt.SetText(annoucememntTxt);
        StartCoroutine(DisableSubtitle(txtDuration));
    }

    public void ShowSubtitle(string speaker, string annoucememntTxt, float txtDuration)
    {
        speakerTxt.SetText(speaker);
        subtitleTxt.gameObject.SetActive(true);
        subtitleTxt.SetText(annoucememntTxt);
        StartCoroutine(DisableSubtitle(txtDuration));
    }

    public void ShowSFXSubtitle(string txt)
    {
        speakerTxt.SetText("");
        subtitleTxt.gameObject.SetActive(true);
        subtitleTxt.SetText(txt);
        StartCoroutine(DisableSubtitle(3));
    }

    public void ShowScientistSubtitle(string txt)
    {
        speakerTxt.SetText("");
        subtitleTxt.gameObject.SetActive(true);
        subtitleTxt.SetText(txt);
        StartCoroutine(DisableSubtitle(5));
    }

    IEnumerator DisableSubtitle(float time)
    {
        yield return new WaitForSeconds(time);
        subtitleTxt.gameObject.SetActive(false);
    }
}
