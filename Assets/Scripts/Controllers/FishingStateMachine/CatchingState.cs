using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class CatchingState : State<FishingControl>
{
    Vector3 runningDirection;
    Vector3 distanceToRod;
    public override void EnterState(FishingControl _owner)
    {
        Debug.Log("CatchingState");
        _owner.Marker.ChangeColor(Color.red);
        _owner.bitingFish = _owner.CatchControl.ChooseFish();
        _owner.bitingFish.Mass = _owner.bitingFish.CalculateFishMass();
        runningDirection = new Vector3(Random.Range(-0.5f, 0.5f), 0.0f, Random.Range(0, 0.5f));

    }

    public override void ExitState(FishingControl _owner)
    {
        _owner.Reel.LineEndurance = 60f;
        _owner.Marker.gameObject.SetActive(true);
        _owner.Reel.StopAnimations();
        _owner.castAnimation.SetBool("casting", false);
        _owner.Bobber.ThrowingRodInWater = false;
        _owner.Marker.ChangeColor(Color.blue);
    }

    public override void UpdateState(FishingControl _owner)
    {
        distanceToRod = _owner.Rod.whatTheRopeIsConnectedTo.transform.position - _owner.Bobber.transform.position;

        _owner.Rod.RotateSpinning();
        //FishFight(_owner);
        _owner.Bending.Bending(true);
        if (Input.GetKey(KeyCode.Mouse0))
        {           
            _owner.Marker.gameObject.SetActive(false);            
            //Pull(_owner);
            _owner.StartCoroutine(LineBreaking(_owner));
            if (_owner.Reel)
                _owner.Reel.CatchingAnimations();
        }
        else
            _owner.Reel.StopAnimations();

        if (distanceToRod.magnitude > 25f)
        {
            _owner.stateMachine.ChangeState(new BrokenLineState());
        }
    }
    public void Pull(FishingControl _owner)
    {        
        
        Vector3 directionXZ = new Vector3(distanceToRod.x, 0, distanceToRod.z);
        _owner.Bobber.Rigidbody.velocity = (directionXZ.normalized * Time.deltaTime * distanceToRod.magnitude * 10f);
        if (distanceToRod.magnitude < 7f)
        {
            _owner.stateMachine.ChangeState(new FishCatchedState());
        }
    }

    public void FishFight(FishingControl _owner)
    {
        _owner.Bobber.Rigidbody.AddForce(runningDirection.normalized * _owner.bitingFish.Mass * Time.deltaTime * 2f, ForceMode.Impulse); //* _owner.bitingFish.FishStrength;
    }

    IEnumerator LineBreaking(FishingControl _owner)
    {
        _owner.Reel.LineEndurance -= _owner.bitingFish.FishStrength * Time.deltaTime;
        if (_owner.Reel.LineEndurance <= 0)
        {
            _owner.stateMachine.ChangeState(new BrokenLineState());
            yield return null;
        }           
        yield return new WaitForSeconds(1f);
    }
}