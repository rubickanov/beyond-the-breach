using AKVA.Assets.Vince.Scripts.SceneManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PASystemTrigger : MonoBehaviour
{
    [SerializeField] string txtAnnoucement;
    [SerializeField] float annoucementDuration;
    [SerializeField] int clipIndex;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SubtitleManager.Instance.PlayPublicAnnoucememnt(txtAnnoucement, clipIndex, annoucementDuration);
            Destroy(gameObject);
        }
    }
}
