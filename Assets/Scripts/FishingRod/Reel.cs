using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    [SerializeField]
    public ChangeLineColor ChangeLine;
    public LineRenderer ReelLine;
    public Transform LinePosition;

    public float MaxLineEndurance;
    public float LineEndurance;

    public Animation AnimationReel;
    AnimationState HandleAnim;
    AnimationState ChelnokAnim;
    AnimationState ShpulyaAnim;
    AnimationState DugkaOpenAnim;
    AnimationState DugkaCloseAnim;

    public float NodeSpeed;

    private void Awake()
    {
        FishingControl.Instance.Rod.Rings.myRings.Insert(0, LinePosition);
        FishingControl.Instance.Rod.Rings.myRings.Add(FishingControl.Instance.Bobber.transform);
        ReelLine.positionCount = FishingControl.Instance.Rod.Rings.myRings.Count;
        ChangeLine = GetComponent<ChangeLineColor>();

        HandleAnim = AnimationReel["Handle_Roll"];
        ChelnokAnim = AnimationReel["Chelnok_Stage"];
        ShpulyaAnim = AnimationReel["Shpulya_Stage"];
        DugkaOpenAnim = AnimationReel["Dugka_Open"];
        DugkaCloseAnim = AnimationReel["Dugka_Close"];
    }

    void LateUpdate()
    {
        Line();
    }

    public void CatchingAnimations()
    {
        ChelnokAnim.enabled = true;
        ChelnokAnim.weight = 1f;
        ChelnokAnim.speed = 0.5f;

        HandleAnim.enabled = true;
        HandleAnim.weight = 1f;
        HandleAnim.speed = 2f;

        ShpulyaAnim.enabled = true;
        ShpulyaAnim.weight = 1f;
    }

    public void OpenDugka()
    {
        DugkaOpenAnim.enabled = true;
        DugkaOpenAnim.weight = 1f;
    }
    public void CloseDugka()
    {
        DugkaCloseAnim.enabled = true;
        DugkaCloseAnim.weight = 1f;
    }

    public void StopAnimations()
    {
        AnimationReel.Stop();
    }

    private void Line()
    {
        for (int i = 0; i < FishingControl.Instance.Rod.Rings.myRings.Count; i++)
            ReelLine.SetPosition(i, FishingControl.Instance.Rod.Rings.myRings[i].position);
    }

    public void LineBreaking()
    {
        LineEndurance -= Time.deltaTime * 2;
        ChangeLine.VizualizeDestroyer((LineEndurance / 2) / MaxLineEndurance);
    }

    //void BezierCurve(LineRenderer Line, Transform point1, Transform point2, Transform point3)
    //{
    //    List<Vector3> bezierList = new List<Vector3>();
    //    for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
    //    {
    //        Transform myPoint = point1;
    //        var tangentLineVertex1 = Vector3.Lerp(point1.position, point2.position, ratio);
    //        var tangentLineVertex2 = Vector3.Lerp(point2.position, point3.position, ratio);
    //        Vector3 bezierpoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
    //        bezierList.Add(bezierpoint);
    //    }
    //    Line.positionCount = bezierList.Count;
    //    Line.SetPositions(bezierList.ToArray());
    //}
}
