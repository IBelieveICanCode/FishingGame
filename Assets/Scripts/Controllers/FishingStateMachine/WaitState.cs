using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class WaitState : State<FishingControl>
{
    float timer = 0;

    public override void EnterState(FishingControl _owner)
    {
        _owner.Reel.CloseDugka();
        Debug.Log("WaitState");
        _owner.Marker.ChangeColor(Color.green);
        _owner.Bobber.transform.Translate(Vector3.down * 0.1f);

    }

    public override void ExitState(FishingControl _owner)
    {

    }

    public override void UpdateState(FishingControl _owner)
    {
        _owner.Bobber.BobberInWater();
        
        _owner.Rod.RotateSpinning();
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _owner.Marker.gameObject.SetActive(false);
            Pull(_owner);
            if (_owner.Reel)
                _owner.Reel.CatchingAnimations();

            timer += Time.deltaTime;
            if (timer >= _owner.CatchControl.ChooseTimeForBite())
            {
                _owner.stateMachine.ChangeState(new CatchingState());
            }
        }
        else
        {
            _owner.Reel.StopAnimations();
            timer = 0f;
        }
    }

    public void Pull(FishingControl _owner)
    {

        Vector3 direction = _owner.Rod.whatTheRopeIsConnectedTo.transform.position - _owner.Bobber.transform.position;
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);
        _owner.Bobber.transform.Translate(directionXZ.normalized * Time.deltaTime);
        if (direction.magnitude < 5f)
        {
            _owner.stateMachine.ChangeState(new IdleState());
        }
    }

}
