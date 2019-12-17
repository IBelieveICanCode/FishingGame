using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class SetState : State<DayNightChange>
{
    public override void EnterState(DayNightChange _owner)
    {
        Debug.Log("SetState");
        AudioController.Instance.NightMusic(2f);
        _owner.ColorChanging(_owner.lightSet, _owner.timeToChangeLightColor);
        _owner.SkyBoxChange("_BlendSet", _owner.SkyboxBlendSetFactor, _owner.timeToChangeSkybox);
    }

    public override void ExitState(DayNightChange _owner)
    {
        _owner.SkyboxBlendDayFactor = 0f;
        RenderSettings.skybox.SetFloat("_BlendDay", _owner.SkyboxBlendDayFactor);
    }

    public override void UpdateState(DayNightChange _owner)
    {
        if (_owner.currentTimeOfDay >= _owner.nightDay)
             _owner.stateMachine.ChangeState(new NightState());
    }
}