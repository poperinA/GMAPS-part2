using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{

    public GameObject player;
    public float Speed = 3.5f;
    float sensitivity = 17f;

    float minFov = 35;
    float maxFov = 100;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(player.transform.position, transform.up, Input.GetAxis("Mouse X") * Speed);
            transform.RotateAround(player.transform.position, transform.right, Input.GetAxis("Mouse Y") * Speed);
        }

        //zoom

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;

    }
}
