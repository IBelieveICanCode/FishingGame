using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendFishingRod : MonoBehaviour
{
    public List<WayPoint> points;    
    public List<Transform> controlPoints;
    public Transform pos0, pos1, pos2, pos3;
    public Transform pos22, pos11;
    public AnimationCurve inPut;
    public AnimationCurve release;

    Vector3 myHead;
    public Transform arrow;
    private Transform target;
    public float MaxPower = 0.4f;
    public float time = 0;
    public float finalizedTIme;
    private float step = 0.1f;
    private void Start()
    {
        target = GameController.Instance.Line.whatIsHangingFromTheRope;
    }

    public void Bending(bool cast) //float power)
    {
        if (cast)
        {
            if (time < 1)
                time += Time.deltaTime * 3;
            finalizedTIme = inPut.Evaluate(time);
        }
        else
        {
            finalizedTIme = release.Evaluate(time);
            if (time > 0)
                time -= Time.deltaTime * 5;
        }
    }

    private void Update()
    {
        //if()
        myHead = target.position - transform.position;
        float distance = myHead.magnitude;
        Vector3 direction = myHead.normalized;
        Quaternion rotation0 = Quaternion.LookRotation(direction);
        arrow.rotation = Quaternion.Euler(rotation0.eulerAngles + new Vector3(90, 0, 0));


        float diff = Quaternion.Angle(transform.rotation, arrow.rotation);

        float normalized = Mathf.Lerp(0, MaxPower, finalizedTIme);

        Vector3 relativePos2 = target.position - pos2.position;
        Quaternion rotation2 = Quaternion.LookRotation(relativePos2);
        pos2.rotation = Quaternion.Slerp(pos22.rotation, Quaternion.Euler(rotation2.eulerAngles + new Vector3(90, 0, 0)), normalized);
        
        Vector3 relativePos3 = target.position - pos1.position;
        Quaternion rotation3 = Quaternion.LookRotation(relativePos3);
        pos1.rotation = Quaternion.Slerp(pos11.rotation, Quaternion.Euler(rotation3.eulerAngles + new Vector3(90, 0, 0)), normalized);
        for (int i = 0; i < points.Count; i++)
        {
            float t0 = ((float)i + 1) / (float)points.Count;
            float t1 = t0 + step;
            Vector3 pos = Mathf.Pow((1 - t0), 3) * pos0.position + 3 * t0 * Mathf.Pow((1 - t0), 2) * pos1.position + 3 * Mathf.Pow(t0, 2) * (1 - t0) * pos2.position + Mathf.Pow(t0, 3) * pos3.position; ;
            Vector3 targetPos = Mathf.Pow((1 - t1), 3) * pos0.position + 3 * t1 * Mathf.Pow((1 - t1), 2) * pos1.position + 3 * Mathf.Pow(t1, 2) * (1 - t1) * pos2.position + Mathf.Pow(t1, 3) * pos3.position; ;
            points[i].point.position = pos;
            Vector3 relativePos = targetPos - points[i].point.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            points[i].point.rotation = Quaternion.Euler(rotation.eulerAngles + new Vector3(90, 0, 0));
        }

    }
}

[System.Serializable]
public struct WayPoint
{
    public Transform point;
    public float time;
}

