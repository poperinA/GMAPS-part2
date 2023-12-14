using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityStrength : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //when jump is pressed
        //start a couroutine
        //the time hold is the gravity strength scale, set a max limit too
    }
}
