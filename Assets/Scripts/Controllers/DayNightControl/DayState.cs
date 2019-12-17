using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class DayState : State<DayNightChange>
{
    public override void EnterState(DayNightChange _owner)
    {
        Debug.Log("DayState");
        AudioController.Instance.DayMusic(2f);
        _owner.ColorChanging(_owner.lightDay, _owner.timeToChangeLightColor);
        _owner.SkyBoxChange("_BlendDay", _owner.SkyboxBlendDayFactor, _owner.timeToChangeSkybox);
    }

    public override void ExitState(DayNightChange _owner)
    {
        _owner.NullifySkyboxValues();
        //_owner.SkyboxBlendMorningFactor = 0f;
        //RenderSettings.skybox.SetFloat("_BlendMorning", _owner.SkyboxBlendMorningFactor);
    }

    public override void UpdateState(DayNightChange _owner)
    {
        if (_owner.currentTimeOfDay >= _owner.setDay)
        {
            _owner.stateMachine.ChangeState(new SetState());
        }
    }
}
