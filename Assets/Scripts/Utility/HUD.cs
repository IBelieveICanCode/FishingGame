using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : Singleton<HUD>
{
    public CatchingHUD CatchingHUD;
    public GameObject LineBroken;

    private void Start()
    {
      
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ShowHideWindow(GameObject hud)
    {
        hud.gameObject.SetActive(true);
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
