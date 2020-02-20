using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace StateStuff
{
    public class CatchingState : State<FishingControl>
    {       
        Vector3 velocityRod;
        Vector3 velocityFish;

        public override void EnterState(FishingControl _owner)
        {
            _owner.Marker.ChangeColor(Color.red);
            _owner.BitingFish = _owner.CatchControl.InitFish();            
            _owner.Bending.CalculateAnimationCurve(_owner.BitingFish.Mass);  
        }

        public override void ExitState(FishingControl _owner)
        {
            _owner.Reel.ChangeLine.VizualizeDestroyer(0);
            _owner.Marker.ChangeColor(Color.blue);
            _owner.Reel.StopAnimations();
            _owner.castAnimation.SetBool("casting", false);
            _owner.Bobber.ThrowingRodInWater = false;
        }

        public override void UpdateState(FishingControl _owner)
        {
            _owner.Marker.MarkerMove();
            _owner.Rod.RotateSpinning();            
            _owner.Bending.Bending(true);
            _owner.Reel.LineBreaking();

            if (Input.GetKey(KeyCode.Mouse0))
            {
                velocityFish = _owner.BitingFish.FishBehaviour.ChosenDirection.PossibleDir.normalized * _owner.BitingFish.FishBehaviour.FishFight(_owner.BitingFish);
                Pull(_owner);                                
                _owner.CatchControl.WeakenFish(0.2f);
                _owner.Reel.CatchingAnimations();
            }
            else
            {
                velocityRod = Vector3.zero;
                velocityFish = _owner.BitingFish.FishBehaviour.ChosenDirection.PossibleDir.normalized * _owner.BitingFish.FishBehaviour.FishMove(_owner.BitingFish);
                _owner.CatchControl.WeakenFish(0.1f);
                _owner.Reel.StopAnimations();
            }

            _owner.Bobber.Rigidbody.velocity = velocityRod + velocityFish;


            if (_owner.Rod.DistanceToBobber.magnitude > _owner.distanceForBreakingLine 
                || _owner.Reel.LineEndurance <= 0)
            {
                _owner.stateMachine.ChangeState(new BrokenLineState());
            }
        }

        public void Pull(FishingControl _owner)
        {
            Vector3 directionXZ = new Vector3(_owner.Rod.DistanceToBobber.x, 0, _owner.Rod.DistanceToBobber.z);
            velocityRod = directionXZ.normalized * Time.fixedDeltaTime * (_owner.Rod.DistanceToBobber.magnitude * 3.5f);            
            if (_owner.Rod.DistanceToBobber.magnitude < _owner.distanceToPullRodTo)
            {
                _owner.stateMachine.ChangeState(new FishCatchedState());
            }
        }
    }
}