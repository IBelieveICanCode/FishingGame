using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour 
{
    [SerializeField]
    private LayerMask waterLayer;
    private ParticleSystem markerParticle;
    private void Awake()
    {
        markerParticle = GetComponent<ParticleSystem>();
    }
    public void MarkerMove()
    {
        Ray directionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(directionRay, out hit, Mathf.Infinity, waterLayer))
        {
            Vector3 point = hit.point;
            transform.position = point;
        }
        Debug.DrawRay(directionRay.origin, directionRay.direction * 1000f, Color.red);
    }

    public void ChangeColor(Color color)
    {
        Gradient gradientColor = new Gradient();
        var originColor = markerParticle.colorOverLifetime;
        gradientColor.SetKeys(new GradientColorKey[] 
        { new GradientColorKey(color, 1.0f) }, 
        new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        originColor.color = gradientColor;
    }
}
