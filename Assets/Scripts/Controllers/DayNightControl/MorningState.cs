using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class MorningState : State<DayNightChange>
{
    public override void EnterState(DayNightChange _owner)
    {
        _owner.ColorChanging(_owner.lightMorning, _owner.timeToChangeLightColor);
        _owner.SkyBoxChange("_BlendMorning", _owner.SkyboxBlendMorningFactor, _owner.timeToChangeSkybox);
    }

    public override void ExitState(DayNightChange _owner)
    {
        _owner.SkyboxBlendNightFactor = 0f;
        RenderSettings.skybox.SetFloat("_BlendNight", _owner.SkyboxBlendNightFactor);
    }

    public override void UpdateState(DayNightChange _owner)
    {
        if (_owner.currentTimeOfDay >= _owner.midDay)
            _owner.stateMachine.ChangeState(new DayState());
    }
}
