using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessSettings : MonoBehaviour
{
    public Color FilterColor = Color.white;
    ColorGrading colorGrading;

    void Start()
    { 
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGrading);
    }

    public void FixedUpdate()
    {
        colorGrading.colorFilter.value = FilterColor;
    }

    public IEnumerator ChangeColor(Color to, float time)
    {
        Color from = FilterColor;
        float speed = 1 / time;
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            FilterColor = Color.Lerp(from, to, percent);
            yield return null;
        }
    }
}
