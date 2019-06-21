using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public BobberPhysic Bobber;
    public CameraControl Camera;
    public FishingControl Player;
    public BendFishingRod Bending;
    public LineInRod Line;
    public Reel Reel;
    public Marker Marker;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }
    }
}
