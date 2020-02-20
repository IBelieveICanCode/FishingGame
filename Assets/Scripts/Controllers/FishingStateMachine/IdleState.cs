using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStuff
{
    public class IdleState : State<FishingControl>
    {
        public override void EnterState(FishingControl _owner)
        {
            Debug.Log("IdleState");
            _owner.Reel.LineEndurance = _owner.Reel.MaxLineEndurance;
            HUD.Instance.CatchingHUD.closedCatchedPanel = false;
            _owner.InitIdleState();
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
                _owner.Marker.gameObject.SetActive(true);               
                Vector3 point = hit.point;
                Vector3 markerPoint = new Vector3(point.x, point.y + 0.05f, point.z);
                _owner.Marker.transform.position = markerPoint;
                return true;
            }
            else
            {
                _owner.Marker.gameObject.SetActive(false);
                return false;
            }
        }
    }
}
