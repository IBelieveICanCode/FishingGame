using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStuff
{
    public class WaitState : State<FishingControl>
    {
        bool timer = true;
        bool bobberTooClose = false;

        public override void EnterState(FishingControl _owner)
        {
            Debug.Log("WaitState");

            bobberTooClose = false;
            _owner.Bobber.CanSeeBobber(false);
            _owner.Reel.CloseDugka();            
            _owner.Marker.ChangeColor(Color.green);
            _owner.Bobber.BobberInWater();
            _owner.Bending.CalculateAnimationCurve(0.1f);

        }

        public override void ExitState(FishingControl _owner)
        {
            //_owner.Marker.gameObject.SetActive(false);
        }

        public override void UpdateState(FishingControl _owner)
        {           
            _owner.Rod.RotateSpinning();
            _owner.Marker.MarkerMove();
            PullRod(_owner);
            if (bobberTooClose)
                _owner.stateMachine.ChangeState(new IdleState());
        }

        private void PullRod(FishingControl _owner)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _owner.Rod.Pull(ref bobberTooClose, _owner.distanceToPullRodTo);
                _owner.Bending.Bending(true);
                _owner.Reel.CatchingAnimations();
                _owner.StartCoroutine(CheckPossibleFish(_owner));
            }
            else
            {
                _owner.Bending.Bending(false);
                _owner.Reel.StopAnimations();
            }
        }

        IEnumerator CheckPossibleFish(FishingControl _owner)
        {
            if (timer)
            {
                timer = false;
                if (Randomizer.CheckProbability(_owner.CatchControl.ChanceToBite))
                    _owner.stateMachine.ChangeState(new CatchingState());
                yield return new WaitForSeconds(1f);
                timer = true;
            }
        }
    }
}
