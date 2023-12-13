using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    float jumpForce = 5f;
    float vertical;
    float horizontal;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);
        

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        Debug.Log($"Is Grounded: {IsGrounded()}");
        Debug.Log($"Velocity: {rb.velocity}");
        Debug.Log($"Position: {transform.position}");

    }

    bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 0.3f;
        LayerMask mask = LayerMask.GetMask("Ground");

        return Physics.Raycast(transform.position, Vector3.down, out hit, distance, mask);

    }
    
}
