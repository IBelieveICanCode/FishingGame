using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class FishingControl : Singleton<FishingControl>
{
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

    public Animator castAnimationComponent;

    public CatchFishControl CatchControl;
    [HideInInspector]
    public Fish bitingFish;

    public StateMachine<FishingControl> stateMachine;

    public Vector3 rodPoint;

    void Start()
    {
        stateMachine = new StateMachine<FishingControl>(this);
        stateMachine.ChangeState(new IdleState());
        castAnimationComponent.runtimeAnimatorController = FishingRod.castAnimation;
        Bobber = Instantiate(FishingRod.Bobber, BobberStartPosition.position, Quaternion.identity);
        
        Rod = Instantiate(FishingRod.Spinning, SpinStartPosition.position, Quaternion.identity);
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
    private void Update()
    {
        stateMachine.Update();
    }

}
