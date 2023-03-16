using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AxisXYZ
{
    X, Y, Z
}
public class RotateScript : MonoBehaviour
{

    public AxisXYZ axisXYZ;

    public float rotateSpeed;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (axisXYZ == AxisXYZ.X)
        {
            transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
        }
        else if (axisXYZ == AxisXYZ.Y)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
        else if (axisXYZ == AxisXYZ.Z)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
    }
}
