using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{

    //array of vectors with different directions of gravity implemented
    public Vector3[] gravityDir = { Vector3.down, Vector3.up, Vector3.right, Vector3.left };
    public int gravityDirIndex = 0;
    
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

        //debug to find out which direction the gravity is pointing
        Debug.Log("gravity direction:" + gravityDir[gravityDirIndex]);

        //set the new gravity direction
        Physics.gravity = gravityDir[gravityDirIndex];

        //implement a rotation during the switching
        StartCoroutine(RotateToGravity(-Physics.gravity.normalized));
        
    }

    //rotate smoothly to the target direction
    IEnumerator RotateToGravity(Vector3 targetDirection)
    {
        //initial rotation of the capsule
        Quaternion startRotation = transform.rotation;
        //finds the target rotation of the capsule using the initial up direction and the targeted direction
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);

        float timeElapsed = 0f;
        float totalLerpTime = 1f;

        //while loop that loops over a period of time (lerp time)
        while (timeElapsed < totalLerpTime)
        {
            //slerp for a smoother rotation between the 2 points
            //interpolate using the ratio of the time
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / totalLerpTime);

            //increase the time for every frame
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Ensure the final rotation is exactly the target rotation
        transform.rotation = targetRotation;
        
    }

}
