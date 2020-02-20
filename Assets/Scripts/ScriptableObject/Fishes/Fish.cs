using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "NewFish", order = 52)]
public class Fish : ScriptableObject
{
    public string FishName;
    public float
        MinFishMass,
        MaxFishMass;
    [HideInInspector]
    public float Mass;    
    [HideInInspector]
    public float Koef;
    public float MaxFishStrength;
    private float fishStrength;
    public float FishStrength { get => fishStrength;
        set
        {            
            //if (fishStrength <= 0.1f)
            //    fishStrength = 0.1f;
            //else
                fishStrength = value;
            
        }
    }
    public Sprite FishSprite;
    public List<StrengthModifiers> StrengthModifiers;

    [SerializeField]
    public FishBehaviour FishBehaviour;

    public float CalculateFishMass()
    {
        Mass = Random.Range(MinFishMass, MaxFishMass);
        return Mass;
    }      
}

[System.Serializable]
public struct StrengthModifiers
{
    public float 
        MinWeight,
        MaxWeight;
    public float KoefFish; 
        
}
