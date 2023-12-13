using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Transform plane;
    public float force = 5f;
    public float gravityStrength = 5f;

    private HVector3D gravityDir, gravityNorm;
    private HVector3D moveDir;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        gravityDir = new HVector3D(plane.position - transform.position);
        gravityNorm = new HVector3D(plane.position - transform.position);
        moveDir = new HVector3D(gravityDir.y, gravityDir.x, gravityDir.z);

        //normalize & flip move vec -> clockwise
        moveDir.Normalize();
        moveDir = moveDir * -1f;

        //add force to move dir
        rb.AddForce(moveDir.ToUnityVector3() * force);

        //normalize gravity vec to get mag = 1 (can set gravity's mag w gravityStrength)
        gravityNorm.Normalize();
        //to get gravity
        rb.AddForce(gravityNorm.ToUnityVector3() * gravityStrength);

        //arrows for gravity vector
        //start position(astronaut), direction pointing in
        DebugExtension.DebugArrow(transform.position, gravityDir.ToUnityVector3(), Color.red);
        //arrows for move direction vector
        DebugExtension.DebugArrow(transform.position, moveDir.ToUnityVector3(), Color.blue);
    }
}
