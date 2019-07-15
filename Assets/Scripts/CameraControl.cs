using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float YDownRotation;
    public float YUpRotation;

    //[Range(1.0f, 10.0f)]
    //public float Xsensitivity;
    int theScreenWidth;
    int theScreenHeight;
    private Vector3 startingMousePosition;
    public int Boundary = 20;
    public int speed = 5;
    // Use this for initialization
    void Start()
    {
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
        startingMousePosition.x = theScreenWidth / 2;
        startingMousePosition.y= theScreenHeight / 2;
    }

    private void FixedUpdate()
    {
        MoveCam();
    }

    void MoveCam()
    {
        Vector3 camPos = transform.position;
        if (Input.mousePosition.x > theScreenWidth - Boundary)
        {
            camPos.x += speed * Time.deltaTime;
            transform.position = camPos;
        }

        if (Input.mousePosition.x < 0 + Boundary)
        {
            camPos.x -= speed * Time.deltaTime;
            transform.position = camPos;
        }

        if (Input.mousePosition.y < 0 + Boundary)
        {
            Quaternion camRot = Quaternion.LookRotation(startingMousePosition - Input.mousePosition);
            camRot.x = -camRot.x;
            camRot.y = 0;
            camRot.z = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, camRot, Time.deltaTime);
            //if (Mathf.Abs(transform.rotation.x) > YUpRotation * Mathf.Deg2Rad)
                //transform.eulerAngles = new Vector3((Mathf.Clamp(transform.rotation.x, YDownRotation, YUpRotation)), 0, 0);
        }
        else if (Input.mousePosition.y > theScreenHeight - Boundary)
        {
            Quaternion camRot = Quaternion.LookRotation(startingMousePosition + Input.mousePosition);
            camRot.y = 0;
            camRot.z = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, camRot, Time.deltaTime);
            if (Mathf.Abs(transform.rotation.x) > YDownRotation * Mathf.Deg2Rad)
            {
                //transform.localRotation = Quaternion.Euler(YUpRotation, 0, 0);
            }              
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0,0), Time.deltaTime);
    }

}
