using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    float vertical;
    float horizontal;

    public HVector3D position = new HVector3D(0, 0, 0);
    public HVector3D velocity = new HVector3D(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
       position.x = transform.position.x;
       position.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        velocity = new HVector3D(horizontal * speed, 0, vertical * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //float u = Mathf.Sqrt(-2);
        }

        Debug.Log($"Input: Horizontal: {horizontal}, Vertical: {vertical}");
        Debug.Log($"PlayerPos: {position.x}, {position.y}, {position.z}");
    }
    public void FixedUpdate()
    {
        UpdatePhysics(Time.fixedDeltaTime);
    }
    private void UpdatePhysics(float deltaTime)
    {
        float displacementX = velocity.x * deltaTime;
        float displacementY = velocity.y * deltaTime;
        float displacementZ = velocity.z * deltaTime; ;

        position.x += displacementX;
        position.y += displacementY;
        position.z += displacementZ;

        transform.position = new Vector3(position.x, position.y, position.z);
    }
}
