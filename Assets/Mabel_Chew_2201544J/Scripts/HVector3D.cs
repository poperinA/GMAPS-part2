using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HVector3D
{
    public float x, y, z;
    public float h;

    public HVector3D(float _x, float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
        h = 1.0f;
    }

    public HVector3D(Vector3 _vec)
    {
        x = _vec.x;
        y = _vec.y;
        z = _vec.z;
        h = 1.0f;
    }

    public HVector3D()
    {
        x = 0;
        y = 0;
        z = 0;
        h = 1.0f;
    }
    public static HVector3D operator +(HVector3D a, HVector3D b)
    {
        return new HVector3D(a.x + b.x, a.y + b.y + a.z, b.z);
    }
    public static HVector3D operator -(HVector3D a, HVector3D b)
    {
        return new HVector3D(a.x - b.x, a.y - b.y, a.z - b.z);
    }   

    public static HVector3D operator *(HVector3D a, float scalar)
    {
        return new HVector3D(a.x * scalar, a.y * scalar, a.z * scalar);
    }

    public static HVector3D operator /(HVector3D a, float scalar)
    {
        return new HVector3D(a.x / scalar, a.y / scalar, a.z / scalar);
    }

    public float Magnitude()
    {
        return Mathf.Sqrt(x * x + y * y + z * z);
    }

    public void Normalize()
    {
        float mag = Magnitude();
        x /= mag;
        y /= mag;
        z /= mag;
    }

    public float DotProduct(HVector3D vec)
    {
        return (x * vec.x + y * vec.y + z * vec.z);
    }

    public HVector3D Projection(HVector3D b)
    {
        HVector3D proj = b * (DotProduct(b) / b.DotProduct(b));
        return proj;
    }

    public float FindAngle(HVector3D vec)
    {
        return (float)Mathf.Acos(DotProduct(vec) / (Magnitude() * vec.Magnitude()));
    }

    public Vector3 ToUnityVector3()
    {
        return new Vector3(x, y, z);
    }

    public void Print()
    {
        Debug.Log("Vector:" + (x, y, z));
    }
}
