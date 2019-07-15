using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour
{
    // The directional light which we manipulate as our sun.
    public Light Sun;
    public Light Moon;

    //public Image BackgroundImage;
    public List<Image> Background;
    Color colorImage;
    Color wantedColor;
    // The number of real-world seconds in one full game day.
    // Set this to 86400 for a 24-hour realtime day.
    public float secondsInFullDay = 120f;

    // The value we use to calculate the current time of day.
    // Goes from 0 (midnight) through 0.25 (sunrise), 0.5 (midday), 0.75 (sunset) to 1 (midnight).
    // We define ourself what value the sunrise sunrise should be etc., but I thought these 
    // values fit well. And now much of the script are hardcoded to these values.
    [Range(0, 1)]
    public float currentTimeOfDay = 0;

    // A multiplier other scripts can use to speed up and slow down the passing of time.
    public float timeMultiplier = 1f;

    // Get the initial intensity of the sun so we remember it.
    float sunInitialIntensity;
    float moonInitialIntensity;
    float intensityMoonMultiplier = 0.1f;
    float intensitySunMultiplier = 1;
    void Start()
    {
        wantedColor = new Color32(31, 38, 75,255);
        colorImage = Color.white;
        sunInitialIntensity = Sun.intensity;
        moonInitialIntensity = Moon.intensity;
    }

    void Update()
    {
        // Updates the sun's rotation and intensity according to the current time of day.
        UpdateSun();

        // This makes currentTimeOfDay go from 0 to 1 in the number of seconds we've specified.
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        // If currentTimeOfDay is 1 (midnight) set it to 0 again so we start a new day.
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        Sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
                     
        if (currentTimeOfDay >= 0.15f)
        {
            //StartCoroutine(ChangeColor(colorImage, 3f));
            intensityMoonMultiplier = Mathf.Clamp01(1 - (currentTimeOfDay - 0.15f) * (1 / 0.02f));
        }
        
        if (currentTimeOfDay >= 0.23f)
        {
            //StartCoroutine(ChangeColor(colorImage, 3f));            
            intensitySunMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        if (currentTimeOfDay > 0.20f && currentTimeOfDay < 0.60f )
        {            
            foreach(Image image in Background)
                StartCoroutine(ChangeColor(image,colorImage, 7f));
        }
        // And fade it out when it sets.
        else if (currentTimeOfDay > 0.77f)
        {
            foreach (Image image in Background)
                StartCoroutine(ChangeColor(image,wantedColor, 5f));
        }
        if (currentTimeOfDay >= 0.80f)
        {
            intensitySunMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.8f) * (1 / 0.02f)));
        }
        if (currentTimeOfDay >= 0.82f)
        {
            intensityMoonMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.82f) * (1 / 0.2f));
        }

        // Multiply the intensity of the sun according to the time of day.
        Sun.intensity = sunInitialIntensity * intensitySunMultiplier;
        Moon.intensity = intensityMoonMultiplier;
    }
    /*
    private void ChangeColor(Color To)
    {
        Color32 currentColor = BackgroundImage.color;
        Color32 newColor = Color32.Lerp(currentColor, To, 1);
        BackgroundImage.color = newColor;
    }
    */
    IEnumerator ChangeColor(Image image, Color to, float time)
    {
        Color32 from = image.color;
        float speed = 1 / time;
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            image.color = Color32.Lerp(from, to, percent);
            yield return null;
        }
    }

}