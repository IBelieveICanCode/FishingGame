using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class IdleState : State<FishingControl>
{
    public override void EnterState(FishingControl _owner)
    {
        Debug.Log("IdleState");
        HUD.Instance.CatchingHUD.closedCatchedPanel = false;
    }

    public override void ExitState(FishingControl _owner)
    {
        
    }

    public override void UpdateState(FishingControl _owner)
    {
        _owner.Rod.RotateSpinning();
        _owner.CameraMovement.MoveCam();
        _owner.Bending.Bending(false);
        if (LookForPlaceToCast(_owner) && Input.GetMouseButtonDown(0))
            _owner.stateMachine.ChangeState(new CastingState());
    }

    public bool LookForPlaceToCast(FishingControl _owner)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _owner.waterLayer))
        {
            Vector3 point = hit.point;
            _owner.Marker.transform.position = point;
            return true;
        }
        else
            return false;

    }
}
