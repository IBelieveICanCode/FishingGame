using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public enum SunTime {Rise, EarlyDay, LateDay, Set, Night }
public class DayNightController : MonoBehaviour
{
    SunTime sunTime;
    // The directional light which we manipulate as our sun.
    public Light Sun;
    public Light Moon;

    public List<Image> Background;
    //public Color dayColor,
    //    darkColor,
    //    dimmColor,
    //    setColor,
    //    nightColor;
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
    private float SkyboxBlendMorningFactor = 0.0f;
    private float SkyboxBlendDayFactor = 0.0f;
    private float SkyboxBlendSetFactor = 0.0f;
    private float SkyboxBlendNightFactor = 0.0f;

    // Get the initial intensity of the sun so we remember it.
    float sunInitialIntensity;
    float moonInitialIntensity;
    float intensityMoonMultiplier = 0.1f;
    float intensitySunMultiplier = 1;
    void Start()
    {
        sunTime = SunTime.EarlyDay;
        sunInitialIntensity = Sun.intensity;
        moonInitialIntensity = Moon.intensity;
        RenderSettings.skybox.SetFloat("_BlendMorning", 0);
        RenderSettings.skybox.SetFloat("_BlendDay", 0);
        RenderSettings.skybox.SetFloat("_BlendSet", 0);
        RenderSettings.skybox.SetFloat("_BlendNight", 0);
    }

    void Update()
    {
        // Updates the sun's rotation and intensity according to the current time of day.
        //UpdateSun();
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        // If currentTimeOfDay is 1 (midnight) set it to 0 again so we start a new day.
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
        DayCycle();
    }

    void DayCycle()
    {
        Sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 0, 0);
        
        Sun.intensity = sunInitialIntensity * intensitySunMultiplier;
        Moon.intensity = intensityMoonMultiplier;
        ChangeTime();
        ControlLight();                
    }

    private void ChangeTime()
    {
        if (currentTimeOfDay >= 0.13f && sunTime == SunTime.Night)
        {
            SunTimeUpdate(SunTime.Rise);
        }
        if (currentTimeOfDay >= 0.23f && sunTime == SunTime.Rise)
        {
            SunTimeUpdate(SunTime.EarlyDay);
        }
        if (currentTimeOfDay > 0.55f && sunTime == SunTime.EarlyDay)
        {
            SunTimeUpdate(SunTime.LateDay);
        }
        if (currentTimeOfDay >= 0.72f && sunTime == SunTime.LateDay)
        {
            SunTimeUpdate(SunTime.Set);
        }
        if (currentTimeOfDay >= 0.78f && sunTime == SunTime.Set)
        {
            SunTimeUpdate(SunTime.Night);
        }
    }

    void ControlLight()
    {
        switch (sunTime)
        {
            case (SunTime.Rise):
                {
                    intensityMoonMultiplier = Mathf.Clamp01(1 - (currentTimeOfDay - 0.13f) * (1 / 0.02f));
                    SkyboxBlendMorningFactor = Mathf.Lerp(0, 1, (currentTimeOfDay - 0.13f) * (1 / 0.024f));
                    if (SkyboxBlendMorningFactor == 1)
                        RenderSettings.skybox.SetFloat("_BlendNight", 0);
                    RenderSettings.skybox.SetFloat("_BlendMorning", SkyboxBlendMorningFactor);
                    break;
                }
            case (SunTime.EarlyDay):
                {
                    intensitySunMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
                    SkyboxBlendMorningFactor = Mathf.Lerp(1, 0, (currentTimeOfDay - 0.23f) * (1 / 0.024f));
                    RenderSettings.skybox.SetFloat("_BlendMorning", SkyboxBlendMorningFactor);
                    //SkyboxBlendFactor = Mathf.Lerp(1, 0, ((currentTimeOfDay - 0.23f) * (1 / 0.05f)));
                    break;
                }
            case (SunTime.LateDay):
                {
                    break;
                }
            case (SunTime.Set):
                {
                    intensitySunMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.72f) * (1 / 0.02f)));
                    SkyboxBlendSetFactor = Mathf.Lerp(0, 1, (currentTimeOfDay - 0.72f) * (1 / 0.02f));
                    RenderSettings.skybox.SetFloat("_BlendSet", SkyboxBlendSetFactor);
                    //SkyboxBlendFactor = Mathf.Lerp(0, 1, (currentTimeOfDay - 0.72f) * (1 / 0.05f));
                    break;
                }
            case (SunTime.Night):
                {
                    intensityMoonMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.78f) * (1 / 0.05f));
                    SkyboxBlendNightFactor = Mathf.Lerp(0, 1, (currentTimeOfDay - 0.78f) * (1 / 0.024f));
                    if (SkyboxBlendNightFactor == 1)
                        RenderSettings.skybox.SetFloat("_BlendSet", 0);
                    RenderSettings.skybox.SetFloat("_BlendNight", SkyboxBlendNightFactor);
                    break;
                }
        }
    }

    void SunTimeUpdate(SunTime newSunTime)
    {
        sunTime = newSunTime;
    }

    void UpdateSun()
    {
        Sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 0, 0);
        //Moon.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) + 150, 0, 0);
        if (currentTimeOfDay >= 0.13f && currentTimeOfDay < 0.20f)
        {
            //Sun.cullingMask = ~ 0;
            intensityMoonMultiplier = Mathf.Clamp01(1 - (currentTimeOfDay - 0.13f) * (1 / 0.02f));
            SkyboxBlendMorningFactor = Mathf.Lerp(0, 1, (currentTimeOfDay - 0.13f) * (1 / 0.024f));
            if (SkyboxBlendMorningFactor == 1)
                RenderSettings.skybox.SetFloat("_BlendNight", 0);
            RenderSettings.skybox.SetFloat("_BlendMorning", SkyboxBlendMorningFactor);
        }

        if (currentTimeOfDay >= 0.23f)
        {            
            intensitySunMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));            
        }
        if (currentTimeOfDay >= 0.23f && currentTimeOfDay < 0.3f )
        {
            SkyboxBlendMorningFactor = Mathf.Lerp(1, 0, (currentTimeOfDay - 0.23f) * (1 / 0.024f));
            RenderSettings.skybox.SetFloat("_BlendMorning", SkyboxBlendMorningFactor);
        }
        //else if (currentTimeOfDay > 0.6f && currentTimeOfDay < 0.7f)
        //{
        //    foreach (Image image in Background)
        //        StartCoroutine(ChangeColor(image, dimmColor, 3f));
        //}
        else if (currentTimeOfDay > 0.72f && currentTimeOfDay < 0.74f)
        {
            SkyboxBlendSetFactor = Mathf.Lerp(0, 1, (currentTimeOfDay - 0.72f) * (1 / 0.02f));
            RenderSettings.skybox.SetFloat("_BlendSet", SkyboxBlendSetFactor);
            
        }

        if (currentTimeOfDay >= 0.76f)
        {
            intensitySunMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.76f) * (1 / 0.02f)));
            
        }
        if (currentTimeOfDay >= 0.76f)
        {
            SkyboxBlendNightFactor = Mathf.Lerp(0, 1, (currentTimeOfDay - 0.76f) * (1 / 0.024f));
            if (SkyboxBlendNightFactor == 1)
                RenderSettings.skybox.SetFloat("_BlendSet", 0);
            RenderSettings.skybox.SetFloat("_BlendNight", SkyboxBlendNightFactor);           
            //intensityMoonMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.76f) * (1 / 0.02f));
        }
        if (currentTimeOfDay >= 0.78f)
        {
            intensityMoonMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.78f) * (1 / 0.02f));
        }
        Sun.intensity = sunInitialIntensity * intensitySunMultiplier;
        Moon.intensity = intensityMoonMultiplier;       
    }    // clumsy version
    
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