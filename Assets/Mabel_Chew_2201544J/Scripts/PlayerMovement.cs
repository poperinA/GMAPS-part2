using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GravitySwitch gravitySwitch;
    public GravityStrength gravityStrengthScript;
    Rigidbody rb;

    float speed = 2f;
    float rotateSpeed = 3f;
    float height = 0.5f;

    float vertical;
    float horizontal;

    float gravityStrength = 9.81f;

    private Vector3 moveDir;

    private Vector3 moveDirNorm;
    private Vector3 changeMovement;
    private Vector3 localChangeMovement;
    private Vector3 velocity;
  
    private Vector3 currentGravityDir;

    bool jumpHold = false;

    

    // Start is called before the first frame update
    void Start()
    {
        //reference to the rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        Move();

        //get the current gravity direction
        currentGravityDir = gravitySwitch.gravityDir[gravitySwitch.gravityDirIndex];


        //gravity force
        rb.AddForce(currentGravityDir * gravityStrength, ForceMode.Acceleration);

        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Debug.Log("space is pressed");
            gravityStrengthScript.ChangeGravityStrength();
            Debug.Log($"Current gravity strength: {gravityStrengthScript.currentGravityStrength}");
            
        }
        if(Input.GetButtonUp("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void Move()
    {
        //get the input of the movement
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        //movement direction
        moveDir = new Vector3(horizontal, 0f, vertical);

        //normalised movement direction
        moveDirNorm = moveDir.normalized;

        //transform the movement direction to align to the current gravity direction
        changeMovement = Quaternion.FromToRotation(-Physics.gravity, -currentGravityDir) * moveDirNorm;

        //TransformDirection -> ignores the position of transform, but considers rotations applied to transform to point in the local "correct" position
        //Without it, eg. Vectors3.left will be pointing in the usual direction of the vector (negative x-axis direction)
        localChangeMovement = transform.TransformDirection(changeMovement);

        //velocity vector
        velocity = localChangeMovement * speed;

        //velocity force
        rb.AddForce(velocity, ForceMode.VelocityChange);

        //checks when the capsule is moving
        if (horizontal != 0 || vertical != 0)
        {
            //updates the direction to look at
            Quaternion rotateTo = Quaternion.LookRotation(localChangeMovement, -currentGravityDir);

            //updates the rotation of the capsule (for looking in the moving direction)
            //use slerp for a smoother rotation between 2 points
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, Time.deltaTime * rotateSpeed);
        }

        if (vertical < 0)
        {
            //updates the backwards direction to look at
            Quaternion backwardRotation = Quaternion.LookRotation(-localChangeMovement, -currentGravityDir);
            transform.rotation = backwardRotation;

        }
    }

    void Jump()
    {
        if(gravityStrengthScript.currentGravityStrength > 0f && IsGrounded())
        {
            //using suvat equation for the jump
            float jumpForce = Mathf.Sqrt(2 * gravityStrengthScript.currentGravityStrength * height);

            //jump force in the opposite direction of gravity
            rb.AddForce(-currentGravityDir * jumpForce, ForceMode.Impulse);

            Debug.Log("Jump");
            gravityStrengthScript.ResetGravityStrength();
        }
        
    }


    bool IsGrounded()
    {
        //info of ray
        RaycastHit hit;
        //distance to check for the hit
        //added an additional 1 as the distance starts from the centre of the capsule
        float distance = 1.1f;
        //make sure the ground only affect the ray
        LayerMask mask = LayerMask.GetMask("Ground");

        //checking if the ray hits the mask(ground) and storing the hit info
        return Physics.Raycast(transform.position, gravitySwitch.gravityDir[gravitySwitch.gravityDirIndex], out hit, distance, mask);
        
    }
}
