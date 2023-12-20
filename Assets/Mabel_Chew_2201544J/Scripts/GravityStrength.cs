using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GravityStrength : MonoBehaviour
{
    
    public float gravityStrength = 9.81f;
    float maxGravityStrength = 20f;
    float rateOfIncrease = 1f;

    public float currentGravityStrength;
    float startGravityStrength;

    // Start is called before the first frame update
    void Start()
    {
        //set the start gravity strength as the gravity strength given
        startGravityStrength = gravityStrength;
    }

    // Update is called once per frame
    void Update()
    {
        //checking the jump is held
        if (Input.GetKey(KeyCode.Space))
        {
            ChangeGravityStrength();
        }

    }

    public void ChangeGravityStrength()
    {
        //ensure that gravity strength doesnt go above max gravity strength
        if (gravityStrength < maxGravityStrength) 
        {
            //increase the gravity strength by the time
            gravityStrength += rateOfIncrease * Time.deltaTime;
            //set the gravity strength 
            currentGravityStrength = gravityStrength;
            
            Debug.Log("current gravity strength:" + currentGravityStrength);
        }

    }

    //for resetting the current gravity strength to initial after jumping
    public void ResetGravityStrength()
    {
        //set the gravity strength as the initial gravity strength
        gravityStrength = startGravityStrength;
        //set the current gravity strength as the gravity strength
        currentGravityStrength = gravityStrength;
    }
}
