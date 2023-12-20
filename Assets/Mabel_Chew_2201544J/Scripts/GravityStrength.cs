using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityStrength : MonoBehaviour
{
    public GravitySwitch gravitySwitch;
    Rigidbody rb;

    public float gravityStrength = 9.81f;
    public float maxGravityStrength = 20f;
    public float gravityChangeDuration = 20f;
    public float currentStrength;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetGravityStrength(gravityStrength);
    }

    // Update is called once per frame
    void Update()
    {
        //when jump is pressed
        //start a couroutine or maybe a for loop?
        //the time hold is the gravity strength scale, set a max limit too
    }

    public IEnumerator ChangeGravityStrength(float maxStrength, float duration)
    {
        float initialStrength = Physics.gravity.magnitude;
        float timeElapsed = 0f;
        float totalLerpTime = 20f;


        while (initialStrength <= maxStrength)
        {
            currentStrength = Mathf.Lerp(initialStrength, maxStrength, timeElapsed / totalLerpTime);
            SetGravityStrength(currentStrength);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        SetGravityStrength(maxStrength);
    }

    public void SetGravityStrength(float strength)
    {
        Physics.gravity = gravitySwitch.gravityDir[gravitySwitch.gravityDirIndex].normalized * strength;
    }

    public float GetGravityStrength()
    {
        return Physics.gravity.magnitude;
    }
}
