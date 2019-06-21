using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineInRod : MonoBehaviour
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
    public Transform whatIsHangingFromTheRope;

    public LineRenderer lineRenderer;

    private void Start()
    {
        ScreenHeight = Screen.height;
        ScreenWidth = Screen.width;
    }
    private void FixedUpdate()
    {
        RotateSpinning();
        DisplayRope();
    }

    private void DisplayRope()
    {
        float ropeWidth = 0.01f;
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        Vector3[] positions = new Vector3[2] {whatTheRopeIsConnectedTo.position, whatIsHangingFromTheRope.position};
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    public void AnimationBobber()
    {
        GameController.Instance.Bobber.CastBobber();
    }

    public void RotateSpinning()
    {
        CursorHeight = Mathf.Clamp(Input.mousePosition.y, 0, ScreenHeight);
        CursorWidth = Mathf.Clamp(Input.mousePosition.x, 0, ScreenWidth);

        NormalizedHeight = CursorHeight / ScreenHeight;
        NormalizedWidth = CursorWidth / ScreenWidth;

        meshSpinning.transform.localRotation = Quaternion.Euler(Mathf.Lerp(MinYValue, MaxYValue, NormalizedHeight), Mathf.Lerp(MinXValue, MaxXValue, NormalizedWidth), 0);
    }
}