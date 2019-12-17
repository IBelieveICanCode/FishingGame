using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FishingRod))]
public class EditorFishing : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        FishingRod fishingScript = (FishingRod)target;
        fishingScript.ExistReel = EditorGUILayout.Toggle("ReelExist", fishingScript.ExistReel);
        if (fishingScript.ExistReel)
        {
            fishingScript.Reel = EditorGUILayout.ObjectField("Reel", fishingScript.Reel, typeof(Reel), true) as Reel;
        }
    }
}
