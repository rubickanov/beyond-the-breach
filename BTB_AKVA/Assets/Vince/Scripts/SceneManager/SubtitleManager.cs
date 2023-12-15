using AKVA.Controls;
using AKVA.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] public AudioClip [] clips;
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

   
    public void PlayPublicAnnoucememnt(string annoucememntTxt, int clipIndex, float txtDuration)
    {
        subtitleTxt.gameObject.SetActive(true);
        subtitleTxt.SetText(annoucememntTxt);
        audioSource.PlayOneShot(clips[clipIndex]);
        StartCoroutine(DisableSubtitle(txtDuration));
    }

    IEnumerator DisableSubtitle(float time)
    {
        yield return new WaitForSeconds(time);
        subtitleTxt.gameObject.SetActive(false);
    }
}
