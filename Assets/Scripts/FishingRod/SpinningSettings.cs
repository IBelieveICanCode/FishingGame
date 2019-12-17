using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSettings : MonoBehaviour
{
    private float ScreenWidth;
    private float ScreenHeight;
    private float CursorWidth;
    private float CursorHeight;
    private float NormalizedWidth;        
    private float NormalizedHeight;

    public float MaxXValue;
    public float MinXValue;
    public float MaxYValue;
    public float MinYValue;

    public GameObject meshSpinning;
    public Transform whatTheRopeIsConnectedTo;
    public Transform ReelPosition;
    private Transform BobberPosition;


    public Rings Rings;


    private void Start()
    {
        ScreenHeight = Screen.height;
        ScreenWidth = Screen.width;
        BobberPosition = FishingControl.Instance.Bobber.transform;
    }
    private void FixedUpdate()
    {
        //RotateSpinning();
    }


    public void RotateSpinning()
    {
        CursorHeight = Mathf.Clamp(Input.mousePosition.y, 0, ScreenHeight);
        CursorWidth = Mathf.Clamp(Input.mousePosition.x, 0, ScreenWidth);

        NormalizedHeight = CursorHeight / ScreenHeight;
        NormalizedWidth = CursorWidth / ScreenWidth;

        meshSpinning.transform.localRotation = Quaternion.Euler(
            Mathf.Lerp(MinYValue, MaxYValue, NormalizedHeight),
            Mathf.Lerp(MinXValue, MaxXValue, NormalizedWidth),
            0);

    }
}