using AKVA.Assets.Vince.Scripts.SceneManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PASystemTrigger : MonoBehaviour
{
    [SerializeField] bool triggerForRobots;
    [SerializeField] string speaker;
    [TextArea]
    public string txtAnnoucement;
    [SerializeField] float annoucementDuration;
    [SerializeField] int clipIndex;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !triggerForRobots)
        {
            SubtitleManager.Instance.PlayPublicAnnoucememnt(speaker, txtAnnoucement, clipIndex, annoucementDuration);
            Destroy(gameObject);
        }else if(other.tag == "Robot")
        {
            SubtitleManager.Instance.PlayPublicAnnoucememnt(speaker, txtAnnoucement, clipIndex, annoucementDuration);
        }
    }

    public void ShowCustomSubTitle()
    {
        SubtitleManager.Instance.ShowSubtitle("[Door Opens]", "Scientist: There he is! Disable that robot!", 11f);
    }
}
