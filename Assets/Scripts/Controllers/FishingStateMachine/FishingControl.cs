using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class FishingControl : Singleton<FishingControl>
{
    public StateMachine<FishingControl> stateMachine;

    [SerializeField]
    private FishingRod FishingRod;
    public CameraControl CameraMovement;
    [Space]
    public Transform BobberStartPosition;
    [HideInInspector]
    public Transform ReelStartPosition;
    public Transform SpinStartPosition;
    [Space]

    public Marker Marker;
    public LayerMask waterLayer;

    [HideInInspector]
    public BendFishingRod Bending;
    [HideInInspector]
    public SpinningSettings Rod;
    [HideInInspector]
    public BobberPhysic Bobber;
    [HideInInspector]
    public Reel Reel;

    public Animator castAnimation;

    public CatchFishControl CatchControl;
    [HideInInspector]
    public Fish BitingFish;
    [Space]
    public float distanceToPullRodTo;
    public float distanceForBreakingLine;
    //private Vector3 runningDirection = Vector3.zero;
    [HideInInspector]
    public Vector3 RunningDirection = Vector3.zero;


    void Awake()
    {
        stateMachine = new StateMachine<FishingControl>(this);        
        castAnimation.runtimeAnimatorController = FishingRod.castAnimation;
        CreateRod(FishingRod);       
        stateMachine.ChangeState(new IdleState());
    }

    private void CreateRod(FishingRod fishingRod)
    {
        Bobber = Instantiate(fishingRod.Bobber, BobberStartPosition.position, Quaternion.identity);

        Rod = Instantiate(fishingRod.Spinning, SpinStartPosition.position, Quaternion.identity);
        Rod.transform.parent = SpinStartPosition;
        Rod.transform.localPosition = Vector3.zero;
        Bending = Rod.GetComponent<BendFishingRod>();

        if (FishingRod.ExistReel)
        {
            Reel = Instantiate(FishingRod.Reel, Rod.ReelPosition.position, Quaternion.identity);
            Reel.transform.parent = Rod.ReelPosition;
            Reel.transform.localPosition = Vector3.zero;
            Reel.transform.localEulerAngles = Vector3.zero;
        }
    }

    public void InitIdleState()
    {
        Bobber.CanSeeBobber(true);
        Marker.gameObject.SetActive(true);
        Reel.StopAnimations();
        castAnimation.SetBool("casting", false);
        Bobber.ThrowingRodInWater = false;
        Reel.ChangeLine.VizualizeDestroyer(0.5f);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}


