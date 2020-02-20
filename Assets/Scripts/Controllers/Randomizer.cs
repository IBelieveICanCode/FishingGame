using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer 
{
    public static bool CheckProbability(float percentage)
    {
        float val = Random.value;
        if (val < percentage/100f)
        {
            return true;
        }
        else
            return false;
    }
}
