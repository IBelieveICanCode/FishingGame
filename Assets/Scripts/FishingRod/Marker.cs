using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour 
{
    [SerializeField]
    private LayerMask waterLayer;
    private ParticleSystem markerParticle;
    private Transform bobberTrans;
    private void Start()
    {
        markerParticle = GetComponent<ParticleSystem>();
        bobberTrans = FishingControl.Instance.Bobber.transform;
    }
    public void MarkerMove()
    {
        Ray directionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(directionRay, out hit, Mathf.Infinity))
        {
            Vector3 markerPos = new Vector3(bobberTrans.position.x, bobberTrans.position.y /*+ 0.01f*/, bobberTrans.position.z);
            transform.position = markerPos;
        }
    }

    public void ChangeColor(Color color)
    {
        Gradient gradientColor = new Gradient();
        var originColor = markerParticle.colorOverLifetime;
        gradientColor.SetKeys(new GradientColorKey[] 
        { new GradientColorKey(color, 1.0f) }, 
        new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        originColor.color = gradientColor;
        //originColor.color = new ParticleSystem.MinMaxGradient(gradientColor);
    }
}
