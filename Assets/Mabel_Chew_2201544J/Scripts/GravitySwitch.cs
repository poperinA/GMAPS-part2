using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public PlayerMovement playerMovement;

    //array of vectors with different directions of gravity implemented
    public Vector3[] gravityDir = { Vector3.down, Vector3.up, Vector3.right, Vector3.left };
    public int gravityDirIndex = 0;

    public Vector3 currentGravityDir;
    public Vector3 newGravityDir;
    

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        

        //check for input for letter Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q key pressed");
            SwitchGravityDir();

        }


    }
    void SwitchGravityDir()
    {
        //loop through every gravity
        gravityDirIndex = (gravityDirIndex + 1) % gravityDir.Length;

        //debug
        Debug.Log($"Switching gravity to: {gravityDir[gravityDirIndex]}");
        DebugExtension.DebugArrow(transform.position, gravityDir[gravityDirIndex], 10f);

        //set the new gravity direction
        Physics.gravity = gravityDir[gravityDirIndex];

        //rotate the player 
        RotateDir(-Physics.gravity.normalized);

        //implement a rotation during the switching

    }

    public void RotateDir(Vector3 gravityDir)
    {

        //capsule's up dir must be equal to the opposite gravity direction
        transform.up = -gravityDir;

        //cross product of the gravity direction and the world up direction
        Vector3 forwardDir = Vector3.Cross(gravityDir, Vector3.up);
        transform.forward = forwardDir;
    }

}