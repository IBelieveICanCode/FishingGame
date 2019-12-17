using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System;

public class DayNightChange : MonoBehaviour
{
    public PostProcessSettings postProcessing;
    [Header("General time Settings")]
    [SerializeField]
    private float secondsInFullDay = 120f;  // Set this to 86400 for a 24-hour realtime day.

    // The value we use to calculate the current time of day.
    // Goes from 0 (midnight) through 0.13 (sunrise), 0.23 (midday), 0.7 (sunset) to 0.85 (night).
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    [SerializeField]
    private float timeMultiplier = 1f; // A multiplier to speed up and slow down the passing of time.

    [Header("Values to change between times of the day")]
    public float timeToChangeLightColor = 3f; // how many seconds will light color be changed
    public float timeToChangeSkybox = 3f; // how many seconds will skybox be changed

    //When the day changes will occure
    [Space]
    [Header("Times of the day")]
    [Range(0, 1f)]
    public float morningDay = 0.13f;
    [Range(0, 1f)]
    public float midDay = 0.23f;
    [Range(0, 1f)]
    public float setDay = 0.72f;
    [Range(0, 1f)]
    public float nightDay = 0.8f;

    // Values to lerp between skyboxes
    [HideInInspector]
    [Header("SkyboxValues")]
    public float 
        SkyboxBlendMorningFactor = 0.0f,
        SkyboxBlendDayFactor = 0.0f,
        SkyboxBlendSetFactor = 0.0f,
        SkyboxBlendNightFactor = 0.0f;

    //Color filter for changing time of the day
    [Header("LightColor")]
    public Color lightMorning;
    public Color lightDay;
    public Color lightSet;
    public Color lightNight;


    public StateMachine<DayNightChange> stateMachine { get; set; }

    private void Start()
    {      
        stateMachine = new StateMachine<DayNightChange>(this);
        NullifySkyboxValues();
        FirstSet();
    }

    void Update()
    {
        CurrentTimeDayChange();
        stateMachine.Update();
    }

    void FirstSet()
    {
        currentTimeOfDay = 0.5f;
        stateMachine.ChangeState(new DayState());
    }

    private void CurrentTimeDayChange()
    {
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;               
        // If currentTimeOfDay is 1 set it to 0 again so we start a new day.
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    public void ColorChanging(Color from, float time)
    {
        StartCoroutine(postProcessing.ChangeColor(from, time));
    }

    #region SkyboxChanging
    public void SkyBoxChange(string skyboxName, float skyboxValue, float time)
    {
        StartCoroutine(SkyBoxValuesChange(skyboxName, skyboxValue, time));
    }

    IEnumerator SkyBoxValuesChange(string skyboxName, float skyboxValue, float time)
    {
        float speed = 1 / time;
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            skyboxValue = Mathf.Lerp(0, 1, percent);
            RenderSettings.skybox.SetFloat(skyboxName, skyboxValue);
            yield return null;
        }        
    }

    public void NullifySkyboxValues()
    {
        RenderSettings.skybox.SetFloat("_BlendMorning", 0);
        RenderSettings.skybox.SetFloat("_BlendDay", 0);
        RenderSettings.skybox.SetFloat("_BlendSet", 0);
        RenderSettings.skybox.SetFloat("_BlendNight", 0);
    }

    #endregion

}
