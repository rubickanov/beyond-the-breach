using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BangingDoor : MonoBehaviour
{
    [SerializeField] float vibrationMagnitude = 0.3f;
    [SerializeField] Transform door;
    private void OnEnable()
    {
        StartCoroutine(StartBanging());
    }

    IEnumerator StartBanging()
    {
        while (true)
        {
            door.transform.localPosition = new Vector3(door.transform.localPosition.x, door.transform.localPosition.y, 0f);
            yield return new WaitForSeconds(vibrationMagnitude);
            door.transform.localPosition = new Vector3(door.transform.localPosition.x, door.transform.localPosition.y, 0.028f);
            yield return new WaitForSeconds(vibrationMagnitude);
        }
    }
}
