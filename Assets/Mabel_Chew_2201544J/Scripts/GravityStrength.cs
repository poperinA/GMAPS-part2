using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityStrength : MonoBehaviour
{
    //public GravitySwitch gravitySwitch;
    //Rigidbody rb;

    //public float gravityStrength = 9.81f;
    //public float maxGravityStrength = 20f;
    //public float gravityChangeDuration = 20f;
    //public float currentStrength;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    SetGravityStrength(gravityStrength);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //when jump is pressed
    //    //start a couroutine
    //    //the time hold is the gravity strength scale, set a max limit too
    //}

    //public IEnumerator ChangeGravityStrength(float maxStrength, float duration)
    //{
    //    float initialStrength = Physics.gravity.magnitude;
    //    float timeElapsed = 0f;
    //    float totalLerpTime = 20f;


    //    while (initialStrength <= maxStrength)
    //    {
    //        currentStrength = Mathf.Lerp(initialStrength, maxStrength, timeElapsed/totalLerpTime);
    //        SetGravityStrength(currentStrength);
    //        timeElapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    SetGravityStrength(maxStrength);
    //}

    //public void SetGravityStrength(float strength)
    //{
    //    Physics.gravity = gravitySwitch.gravityDir[gravitySwitch.gravityDirIndex].normalized * strength;
    //}

    //public float GetGravityStrength()
    //{
    //    return Physics.gravity.magnitude;
    //}

    Rigidbody rb;
    bool isJumpButtonPressed = false;

    // Adjust this value as needed
    float maxGravityStrength = 20f;
    float gravityStrengthDecreaseRate = 2f;
    private float gravityStrength = 9.81f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the jump button is pressed
        if (Input.GetButtonDown("Jump"))
        {
            isJumpButtonPressed = true;
            StartCoroutine(DecreaseGravityStrength());
        }

        // Check if the jump button is released
        if (Input.GetButtonUp("Jump"))
        {
            isJumpButtonPressed = false;
        }
    }

    //IEnumerator DecreaseGravityStrength()
    //{
    //    while (isJumpButtonPressed && gravityStrength > 0f)
    //    {
    //        gravityStrength -= gravityStrengthDecreaseRate * Time.deltaTime;

    //        // Clamp gravity strength to avoid going below zero
    //        gravityStrength = Mathf.Max(0f, gravityStrength);

    //        // Apply the updated gravity strength to the Rigidbody
    //        rb.gravityScale = gravityStrength / maxGravityStrength;

    //        yield return null;
    //    }
    //}
}
