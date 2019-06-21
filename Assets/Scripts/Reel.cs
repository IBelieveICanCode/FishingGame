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

    public LineRenderer Line;

    public Transform LineNodeController1;

    public AnimationCurve LineNodeCurve;
    public float NodeSpeed;
    public Transform LineNode0, LineNode1, LineNode2, LineNode3, LineNode4, LineNode5;

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
                LineNodeController1.localEulerAngles = LineNodeController1.forward * Mathf.Lerp(-35, 35, LineNodeCurve.Evaluate(caster.localEulerAngles.y / 360));
            }

            Line.SetPosition(0, LineNode0.position);
            Line.SetPosition(1, LineNode1.position);
            Line.SetPosition(2, LineNode2.position);
            Line.SetPosition(3, LineNode3.position);
            Line.SetPosition(4, LineNode4.position);
            Line.SetPosition(5, LineNode5.position);

            if (time < 1)
                time += Time.deltaTime * openSpeed;
            rama.localRotation = Quaternion.Slerp(closePos.localRotation, openPos.localRotation, time);
    }
}
