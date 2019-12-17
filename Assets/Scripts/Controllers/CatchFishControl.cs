using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFishControl : MonoBehaviour
{
    public List<Fish> AvailableFishes;

    public float
        MinBite,
        MaxBite;

    public Fish ChooseFish()
    {
        return AvailableFishes[Random.Range(0, AvailableFishes.Count)];
    }

    public float ChooseTimeForBite()
    {
        return Random.Range(MinBite, MaxBite);
    }
}
