using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private float CameraHeight = 10f;
    [SerializeField] private Transform FollowPlayer;

    private void Update()
    {
        Vector3 cameraPostion = FollowPlayer.position;
        cameraPostion.y += CameraHeight;
        transform.position = cameraPostion;
    }
}
