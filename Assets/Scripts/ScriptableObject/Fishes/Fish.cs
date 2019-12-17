using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "CreateFish", order = 52)]
public class Fish : ScriptableObject
{
    public string FishName;
    public float
        MinFishMass,
        MaxFishMass;
    [HideInInspector]
    public float Mass;
    public float FishStrength = 5f;

    public Sprite FishSprite;

    public float CalculateFishMass()
    {
        Mass = Random.Range(MinFishMass, MaxFishMass);
        return Mass;
    }
}
