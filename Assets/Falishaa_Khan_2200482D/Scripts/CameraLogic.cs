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
        //checks if the right mouse button is held down
        if (Input.GetMouseButton(1))
        {
            //rotate the camera around the player based on mouse input
            transform.RotateAround(player.transform.position, transform.up, Input.GetAxis("Mouse X") * Speed);
            transform.RotateAround(player.transform.position, transform.right, Input.GetAxis("Mouse Y") * Speed);
        }

        //zoom
        float fov = Camera.main.fieldOfView;

        //adjust the field of view based on the mouse scroll wheel input
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;

        //clamp the field of view to the specified range
        fov = Mathf.Clamp(fov, minFov, maxFov);

        //apply the updated field of view to the camera
        Camera.main.fieldOfView = fov;

    }
}
