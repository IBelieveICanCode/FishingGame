using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript 
{
    float acceleration;
    public static Vector3 CalculateGravity()
    {
        return Physics.gravity * Mathf.Pow(Time.fixedDeltaTime , 2) / 2;
    }
}
