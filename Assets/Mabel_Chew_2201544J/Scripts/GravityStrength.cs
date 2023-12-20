using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityStrength : MonoBehaviour
{
    public GravitySwitch gravitySwitch;
    public PlayerMovement playerMovement;
    Rigidbody rb;

    public float gravityStrength = 9.81f;
    public float minGravityStrength = 1f;
    //public float gravityChangeDuration = 20f;
    public float currentStrength;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //when jump is pressed
        //start a couroutine or maybe a for loop?
        //the time hold is the gravity strength scale, set a max limit too



    }

    

    void ChangeGravityStrength()
    {
        
    }
}
