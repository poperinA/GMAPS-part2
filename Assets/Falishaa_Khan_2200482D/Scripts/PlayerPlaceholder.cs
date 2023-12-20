using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceholder : MonoBehaviour
{
    public GameObject Player;
    public GameObject Planet;

    void Update()
    {
        //postion: move towards the player's position with interpolation
        transform.position = Vector3.Lerp(transform.position, Player.transform.position, 0.1f);

        //gravitational direction
        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

        //rotation: adjust the rotation to align with the gravitational direction
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f);
    }

    //change the reference to the current planet
    public void NewPlanet(GameObject newPlanet)
    {
        Planet = newPlanet;
    }
}
