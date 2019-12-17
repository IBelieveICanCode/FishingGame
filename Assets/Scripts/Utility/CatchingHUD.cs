using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchingHUD : MonoBehaviour
{
    [SerializeField]
    private Image catchedFishingImage;

    [SerializeField]
    private Text
        catchedFishName,
        catchedFishMass;
    [SerializeField]
    private Button
        toTheBagButton,
        letItGoButton;

    public bool closedCatchedPanel;

    public void ShowFishData(Fish fish)
    {
        catchedFishingImage.sprite = fish.FishSprite;
        catchedFishName.text = fish.FishName;
        catchedFishMass.text = (Mathf.Round(fish.Mass * 100f)/ 1000).ToString() + " кг" ;
    }

    public void ChangeState()
    {
        closedCatchedPanel = !closedCatchedPanel;
    }
}
