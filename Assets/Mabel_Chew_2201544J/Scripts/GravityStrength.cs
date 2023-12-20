using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GravityStrength : MonoBehaviour
{
    Rigidbody rb;
    public float gravityStrength = 9.81f;
    public float minGravityStrength = 1f;
    public float rateOfDecrease = 10f;
    public float currentGravityStrength;

    private float startGravityStrength;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startGravityStrength = gravityStrength;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            ChangeGravityStrength();
        }

    }

    public void ChangeGravityStrength()
    {
        if (gravityStrength > minGravityStrength) 
        {
            //decrease the gravity strength by the time
            gravityStrength -= rateOfDecrease * Time.deltaTime;
            //set the gravity strength 
            currentGravityStrength = gravityStrength;
            
            Debug.Log($"Current gravity strength: {currentGravityStrength}");
        }
    }

    public void ResetGravityStrength()
    {
        gravityStrength = startGravityStrength;
        currentGravityStrength = gravityStrength;
    }
}
