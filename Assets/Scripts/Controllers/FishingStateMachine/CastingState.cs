using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class CastingState : State<FishingControl>
{
    public override void EnterState(FishingControl _owner)
    {
        Debug.Log("CastingState");
        _owner.castAnimationComponent.SetBool("casting", true);        
        _owner.Reel.OpenDugka();
    }

    public override void ExitState(FishingControl _owner)
    {
    }

    public override void UpdateState(FishingControl _owner)
    {
        
        if (_owner.castAnimationComponent.GetCurrentAnimatorStateInfo(0).IsName("CastingAnimationEnd"))
        {            
            _owner.Bobber.CastBobber(30f);
            Vector3 target = _owner.Marker.transform.position - _owner.Bobber.transform.position;
            float angle = Vector3.Angle(_owner.Bobber.transform.position, target);
            _owner.Bobber.transform.eulerAngles = new Vector3(angle, 0f, 0f);
        }
        
        if (Vector3.Distance(_owner.Bobber.transform.position, _owner.Marker.transform.position) <= 0.1f)
        {
            _owner.Bobber.transform.eulerAngles = Vector3.zero;
            _owner.stateMachine.ChangeState(new WaitState());

        }
    }

}