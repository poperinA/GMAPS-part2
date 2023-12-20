using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GravitySwitch gravitySwitchScript;
    public GravityStrength gravityStrengthScript;
    Rigidbody rb;

    float speed = 1.5f;
    float rotateSpeed = 3f;
    float height = 0.5f;

    float vertical;
    float horizontal;

    private Vector3 moveDir;
    private Vector3 moveDirNorm;
    private Vector3 changeMovement;
    private Vector3 localChangeMovement;
    private Vector3 velocity;
    private Vector3 currentGravityDir;
    

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
        currentGravityDir = gravitySwitchScript.gravityDir[gravitySwitchScript.gravityDirIndex];

        
        //gravity force
        rb.AddForce(currentGravityDir * gravityStrengthScript.gravityStrength, ForceMode.Acceleration);
        
    }

    private void Update()
    {
        //if space is held down and is touching the ground
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            
            Debug.Log("space is pressed");
            //the gravity strength will change with this method
            gravityStrengthScript.ChangeGravityStrength();
            
            Debug.Log("current gravity strength:" + gravityStrengthScript.currentGravityStrength);

        }
        //if space is released, then jump method will be called
        if(Input.GetKeyUp(KeyCode.Space))
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
        //checks when the capsule is moving backwards
        if (vertical < 0)
        {
            //updates the direction to look at (backwards -> negative)
            Quaternion backwardRotation = Quaternion.LookRotation(-localChangeMovement, -currentGravityDir);
            //set the rotation of the capsule
            transform.rotation = backwardRotation;

        }
    }

    void Jump()
    {
        //only jump when gravity strength is more than 0 and is touching the ground
        if(gravityStrengthScript.currentGravityStrength > 0f && IsGrounded())
        {
            //using suvat equation for the jump (v^2 = u^2 + 2as)
            float jumpForce = Mathf.Sqrt(2 * gravityStrengthScript.currentGravityStrength * height);

            //jump force in the opposite direction of gravity
            rb.AddForce(-currentGravityDir * jumpForce, ForceMode.Impulse);

            Debug.Log("Jump");
            //after each jump, call this method
            gravityStrengthScript.ResetGravityStrength();
        }
        
    }

    bool IsGrounded()
    {
        //distance to check for the hit
        //added an additional 1 as the distance starts from the centre of the capsule
        float distance = 1.1f;
        //make sure the ground only affect the ray
        LayerMask mask = LayerMask.GetMask("Ground");

        //checking if the ray hits the mask(ground)
        return Physics.Raycast(transform.position, gravitySwitchScript.gravityDir[gravitySwitchScript.gravityDirIndex], distance, mask);
        
    }
}
