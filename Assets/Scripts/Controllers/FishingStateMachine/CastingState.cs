using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStuff
{
    public class CastingState : State<FishingControl>
    {
        public override void EnterState(FishingControl _owner)
        {
            Debug.Log("CastingState");
            _owner.castAnimation.SetBool("casting", true);
            _owner.Reel.OpenDugka();
        }

        public override void ExitState(FishingControl _owner)
        {
        }

        public override void UpdateState(FishingControl _owner)
        {

            if (_owner.castAnimation.GetCurrentAnimatorStateInfo(0).IsName("CastingAnimationEnd"))
            {
                _owner.Bobber.CastBobber(45f);               
            }

            if (Vector3.Distance(_owner.Bobber.transform.position, _owner.Marker.transform.position) <= 0.35f)
            {                
                _owner.stateMachine.ChangeState(new WaitState());
            }
        }

    }
}