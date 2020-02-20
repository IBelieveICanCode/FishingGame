using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLineChange : ChangeLineColor
{
    public override void VizualizeDestroyer(float koef)
    {
        Color color = new Color(0.5f, koef, koef);
        lineRenderer.material.SetColor("_EmissionColor", color);
    }
}
