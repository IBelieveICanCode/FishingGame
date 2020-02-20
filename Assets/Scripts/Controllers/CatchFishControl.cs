using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFishControl : MonoBehaviour
{
    public List<Fish> AvailableFishes;

    public float ChanceToBite;

    bool timer = true;

    public Fish InitFish()
    {
        Fish _newFish = ChooseFish();
        _newFish.Mass = _newFish.CalculateFishMass();
        _newFish.FishStrength = _newFish.MaxFishStrength;
        for (int i = 0; i < _newFish.StrengthModifiers.Count; i++)
        {            
            if (_newFish.Mass > _newFish.StrengthModifiers[i].MinWeight)
            {
                if (_newFish.Mass < _newFish.StrengthModifiers[i].MaxWeight)
                {
                    _newFish.Koef = _newFish.StrengthModifiers[i].KoefFish;
                }
            }
        }
        _newFish.FishBehaviour.timer = true;
        _newFish.FishBehaviour.ChooseFishDirection();
        return _newFish;
    }

    public Fish ChooseFish()
    {
        return AvailableFishes[Random.Range(0, AvailableFishes.Count)];
    }

    public void WeakenFish(float _weakKoef)
    {
        StopCoroutine(SlowlyWeaken(_weakKoef));
        StartCoroutine(SlowlyWeaken(_weakKoef));
    }

    private IEnumerator SlowlyWeaken(float _weakKoef)
    {
        if (timer)
        {
            timer = false;
            //_fish.FishStrength -= _weakKoef;
            FishingControl.Instance.BitingFish.FishStrength -= _weakKoef;
            yield return new WaitForSeconds(1f);
            timer = true;
        }
    }
}
