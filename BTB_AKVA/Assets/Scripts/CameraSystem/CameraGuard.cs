using System;
using UnityEngine;

namespace AKVA.CameraSystem
{
    public class CameraGuard : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Camera cam;

        private bool IsSpotted(Camera c, Transform target)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(c);
            Vector3 point = target.position;

            foreach (Plane plane in planes)
            {
                if (plane.GetDistanceToPoint(point) < 0)
                {
                    return false;
                }
            }
            return true;
        }


        private void Update()
        {
            if (IsSpotted(cam, target))
            {
                Debug.Log("SPOTTED");
            }
        }

    }
}
