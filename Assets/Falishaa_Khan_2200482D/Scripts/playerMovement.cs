using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    public bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation so the player doesn't topple over
    }

    void Update()
    {
        MovePlayer();
        Jump();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * vertical);
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * horizontal);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        float horizontalRotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        float verticalRotation = Input.GetAxis("Vertical") * rotationSpeed * Time.fixedDeltaTime;

        // Rotate around the up axis (Y) for horizontal movement
        rb.AddTorque(Vector3.up * horizontalRotation);

        // Rotate around the right axis (X) for vertical movement
        rb.AddTorque(Vector3.right * verticalRotation);
    }
}
