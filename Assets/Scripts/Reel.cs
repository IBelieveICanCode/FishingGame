using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    public float BarabanSpeed;
    public float Speed;
    public bool Casting = true;

    public Transform caster;

    public float handleSpeed;
    public Transform handle;


    public Transform rama;
    public Transform openPos;
    public Transform closePos;

    public Transform baraban;
    public Transform originPos;

    public AnimationCurve barabanCurve;
    public float amplitude = 1;


    public float openSpeed;
    public float closeSpeed;

    public float time;

    public float rotTime;
    public float rotTime2;
    public LineRenderer StartingLine, BezierLine1, BezierLine2, EndingLine;
    public int vertexCount = 12; // for bezier
    //public Transform LineNodeController;
    //public AnimationCurve LineNodeCurve;

    public float NodeSpeed;
    public Transform LineNode0, LineNode1, LineNode2, LineNode3, LineNode4, LineNode5, ExtraNode, ExtraNode1;

    void Update()
    {
        ReelWorking(false, Vector3.zero, Vector3.zero);
    }

    public void ReelWorking(bool reel, Vector3 casterRot, Vector3 handleRot)
    {
        if (reel)
        {
            rotTime += Time.deltaTime * BarabanSpeed;
            rotTime2 += Time.deltaTime * NodeSpeed;
            caster.Rotate(casterRot * Speed);
            handle.Rotate(handleRot * handleSpeed);
            baraban.localPosition = originPos.localPosition + (Vector3.up * barabanCurve.Evaluate(rotTime) * amplitude);
            //LineNodeController.localEulerAngles = LineNodeController.forward * Mathf.Lerp(-35, 35, LineNodeCurve.Evaluate(caster.localEulerAngles.y / 360));
        }
        if (GameController.Instance.Player.State == PlayerState.Idle)
        {
            StartingLine.positionCount = 2;
            EndingLine.positionCount = 3;
            BezierLine1.gameObject.SetActive(true);
            BezierLine2.gameObject.SetActive(true);
            EndingLine.gameObject.SetActive(true);
            StartingLine.SetPosition(0, LineNode0.position);
            StartingLine.SetPosition(1, LineNode1.position);
            BezierCurve(BezierLine1, LineNode1, ExtraNode, LineNode2);
            BezierCurve(BezierLine2, LineNode2, ExtraNode1, LineNode3);
            EndingLine.SetPosition(0, LineNode3.position);
            EndingLine.SetPosition(1, LineNode4.position);
            EndingLine.SetPosition(2, LineNode5.position);           
        }
        else
        {
            StartingLine.positionCount = 6;
            BezierLine1.gameObject.SetActive(false);
            BezierLine2.gameObject.SetActive(false);
            EndingLine.gameObject.SetActive(false);
            StartingLine.SetPosition(0, LineNode0.position);
            StartingLine.SetPosition(1, LineNode1.position);
            StartingLine.SetPosition(2, LineNode2.position);
            StartingLine.SetPosition(3, LineNode3.position);
            StartingLine.SetPosition(4, LineNode4.position);
            StartingLine.SetPosition(5, LineNode5.position);
        }
        if (time < 1)
            time += Time.deltaTime * openSpeed;
        rama.localRotation = Quaternion.Slerp(closePos.localRotation, openPos.localRotation, time);
    }

    void BezierCurve(LineRenderer Line, Transform point1, Transform point2, Transform point3)
    {
        List<Vector3> bezierList = new List<Vector3>();
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
        {
            Transform myPoint = point1;
            var tangentLineVertex1 = Vector3.Lerp(point1.position, point2.position, ratio);
            var tangentLineVertex2 = Vector3.Lerp(point2.position, point3.position, ratio);
            Vector3 bezierpoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
            bezierList.Add(bezierpoint);
        }
        Line.positionCount = bezierList.Count;
        Line.SetPositions(bezierList.ToArray());
    }
}
