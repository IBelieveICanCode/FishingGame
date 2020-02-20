using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float YDownMax = 25f;
    public float YUpMax = -20f;

    public float LeftCamBorder;
    public float RightCamBodrer;
    //[Range(1.0f, 10.0f)]
    //public float Xsensitivity;
    int theScreenWidth;
    int theScreenHeight;
    private float rotateX;
    private float Xrotation;
    private Vector3 startingMousePosition;
    Vector3 startCamPos;
    Vector3 startCamRot;

    public int Boundary = 20;
    public int speed = 5;
    // Use this for initialization
    void Start()
    {
        startCamPos = transform.position;
        startCamRot = transform.eulerAngles;
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
        startingMousePosition.x = theScreenWidth / 2;
        startingMousePosition.y= theScreenHeight / 2;
    }

    public void MoveCam()
    {        
        if (Input.GetKey(KeyCode.RightArrow))//Input.mousePosition.x > theScreenWidth - Boundary)
        {
            startCamPos.x += speed * Time.deltaTime;
            FishingControl.Instance.castAnimation.SetBool("moving", true);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))//Input.mousePosition.x < 0 + Boundary)
        {
            startCamPos.x -= speed * Time.deltaTime;
            FishingControl.Instance.castAnimation.SetBool("moving", true);
        }
        else
            FishingControl.Instance.castAnimation.SetBool("moving", false);

        transform.position = new Vector3(startCamPos.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(
                                Mathf.Clamp(transform.position.x, LeftCamBorder, RightCamBodrer),
                                transform.position.y, transform.position.z);
        
    }

}
