using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fishing Rod", menuName = "Fishing Rod", order = 51)]
public class FishingRod : ScriptableObject
{
    public float timeToBite;

    [HideInInspector]
    public bool ExistReel;
    [HideInInspector]
    public Reel Reel;
    //public BendFishingRod Bending;
    //public BobberPhysic Bobber;
    //public Marker Marker;

    public RuntimeAnimatorController castAnimation;

    public BobberPhysic Bobber;
    public SpinningSettings Spinning;
}
