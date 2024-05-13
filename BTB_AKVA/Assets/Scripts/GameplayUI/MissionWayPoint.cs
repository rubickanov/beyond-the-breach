using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionWayPoint : MonoBehaviour
{
    public static MissionWayPoint Instance { get; private set; }

    public Transform playerCamera;
    public Image wayPointImg;
    public Transform target;
    public TextMeshProUGUI meter;
    public Vector3 offset;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (target.position == null || playerCamera == null) return;
        // Giving limits to the icon so it sticks on the screen
        // Below calculations witht the assumption that the icon anchor point is in the middle
        // Minimum X position: half of the icon width
        float minX = wayPointImg.GetPixelAdjustedRect().width / 2;
        // Maximum X position: screen width - half of the icon width
        float maxX = Screen.width - minX;

        // Minimum Y position: half of the height
        float minY = wayPointImg.GetPixelAdjustedRect().height / 2;
        // Maximum Y position: screen height - half of the icon height
        float maxY = Screen.height - minY;

        // Temporary variable to store the converted position from 3D world point to 2D screen point
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        // Check if the target is behind us, to only show the icon once the target is in front
        if (Vector3.Dot((target.position - playerCamera.position), playerCamera.forward) < 0)
        {
            // Check if the target is on the left side of the screen
            if (pos.x < Screen.width / 2)
            {
                // Place it on the right (Since it's behind the player, it's the opposite)
                pos.x = maxX;
            }
            else
            {
                // Place it on the left side
                pos.x = minX;
            }
        }

        // Limit the X and Y positions
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Update the marker's position
        wayPointImg.transform.position = pos;
        // Change the meter text to the distance with the meter unit 'm'
        meter.text = ((int)Vector3.Distance(target.position, playerCamera.position)).ToString() + "m";
    }

    public void SetMarkerLocation(Transform location)
    {
        SetActiveWayPointMarker(true);
        target = location;
    }

    public void SetActiveWayPointMarker(bool active)
    {
        if (active)
        {
            wayPointImg.gameObject.SetActive(true);
        }
        else
        {
            wayPointImg.gameObject.SetActive(false);
        }
    }
}
