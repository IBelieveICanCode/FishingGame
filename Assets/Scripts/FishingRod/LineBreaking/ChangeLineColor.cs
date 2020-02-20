using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangeLineColor : MonoBehaviour
{
    [SerializeField]
    protected LineRenderer lineRenderer;
    protected Material material;

    public abstract void VizualizeDestroyer(float koef);
}
