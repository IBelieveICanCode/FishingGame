using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStuff
{
    public class FishCatchedState : State<FishingControl>
    {
        public override void EnterState(FishingControl _owner)
        {
            Debug.Log("FishCatchedState");
            HUD.Instance.CatchingHUD.ShowFishData(_owner.BitingFish);
            HUD.Instance.ShowHideWindow(HUD.Instance.CatchingHUD.gameObject);
            _owner.Bobber.transform.eulerAngles = Vector3.zero;
        }

        public override void ExitState(FishingControl _owner)
        {
        }

        public override void UpdateState(FishingControl _owner)
        {
            _owner.Bending.Bending(false);
            if (HUD.Instance.CatchingHUD.closedCatchedPanel)
            {
                _owner.stateMachine.ChangeState(new IdleState());
            }
        }

    }
}
