using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour: MonoBehaviour 
{
    public bool timer = true;
    [HideInInspector]
    public DirectionForFish ChosenDirection;

    protected DirectionForFish[] DirectionsForFish = new DirectionForFish[]
    {
        new DirectionForFish(Vector3.forward, true, 10f),
        new DirectionForFish(Vector3.back, false, 100f),
        new DirectionForFish(Vector3.right, true, 10f),
        new DirectionForFish(Vector3.left, false, 10f),
    };


    public virtual void ChooseFishDirection()
    {
        //DirectionForFish _dir = new DirectionForFish();
        List<DirectionForFish> _chosenDirections = new List<DirectionForFish>();
        for (int i = 0; i < DirectionsForFish.Length; i++)
        {
            //_dir = i;
            DirectionsForFish[i].Accessible = !DirectionsForFish[i].Accessible;
            if (DirectionsForFish[i].Accessible)
            {
                _chosenDirections.Add(DirectionsForFish[i]);
            }
        }
        ChosenDirection = _chosenDirections[Random.Range(0, _chosenDirections.Count)];
        foreach (var i in _chosenDirections)
            print(i.PossibleDir);
    }

    public virtual float FishFight(Fish fish)
    {
        FishingControl.Instance.StartCoroutine(CheckDirectionFish());
        return /*ChosenDirection.PossibleDir.normalized*/ fish.Mass * Time.deltaTime * (fish.FishStrength * fish.Koef);        
    }

    public virtual float FishMove(Fish fish)
    {
        FishingControl.Instance.StartCoroutine(CheckDirectionFish());
        return fish.Koef;
    }


    public virtual IEnumerator CheckDirectionFish()
    {
        if (timer)
        {            
            timer = false;
            if (Randomizer.CheckProbability(ChosenDirection.ChanceToChangeDirection))//_owner.CatchControl.ChanceForFishToChangeDir))
            {
                ChooseFishDirection();
            }
            yield return new WaitForSeconds(0.5f);
            timer = true;
        }
    }

}

public struct DirectionForFish
{
    public Vector3 PossibleDir;
    public bool Accessible;
    public float ChanceToChangeDirection;

    public DirectionForFish(Vector3 dir, bool access, float dirChance)
    {
        PossibleDir = dir;
        Accessible = access;
        ChanceToChangeDirection = dirChance;
    }
}


