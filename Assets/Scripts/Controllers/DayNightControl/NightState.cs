using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class NightState : State<DayNightChange>
{
    public override void EnterState(DayNightChange _owner)
    {
        _owner.ColorChanging(_owner.lightNight, _owner.timeToChangeLightColor);
        _owner.SkyBoxChange("_BlendNight", _owner.SkyboxBlendNightFactor, _owner.timeToChangeSkybox);
    }

    public override void ExitState(DayNightChange _owner)
    {
        _owner.SkyboxBlendSetFactor = 0f;
        RenderSettings.skybox.SetFloat("_BlendSet", _owner.SkyboxBlendSetFactor);
    }

    public override void UpdateState(DayNightChange _owner)
    {

        if (_owner.currentTimeOfDay >= _owner.morningDay && _owner.currentTimeOfDay < _owner.midDay)
           _owner.stateMachine.ChangeState(new MorningState());
    }
}
