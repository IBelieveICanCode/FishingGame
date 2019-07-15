using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Cast, Wait, Biting, Catching, Catched }
public class FishingControl : MonoBehaviour
{
    GameController Controller;
    public PlayerState State;
    [SerializeField]
    private Animator castAnimation;
    float timeForBite;
    float timerToBite = 0;

    void Start()
    {
        Controller = GameController.Instance;
        UpdateState(PlayerState.Idle);
    }

    private void Update()
    {
        switch (State)
        {
            case PlayerState.Idle:
                CastControl();
                Controller.Bending.Bending(false);
                LookForPlaceToCast();
                break;
            case PlayerState.Cast:
                timeForBite = UnityEngine.Random.Range(2f, 5f);
                Casting();
                break;
            case PlayerState.Wait:
                Controller.Bobber.BobberInWater();
                Controller.Marker.ChangeColor(Color.green);
                Waiting();
                break;
            case PlayerState.Biting:
                Controller.Bobber.BobberInWater();
                Controller.Marker.ChangeColor(Color.red);
                GameController.Instance.Bobber.CatchingFish();
                break;
            case PlayerState.Catching:
                Catching();
                break;
        }
    }

    private void CastControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateState(PlayerState.Cast);
        }
    }

    public void UpdateState(PlayerState newState)
    {
        State = newState;
    }

    #region Idle
    private void LookForPlaceToCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Water")
            {
                Controller.Marker.MarkerMove();
                Controller.Marker.gameObject.SetActive(true);
            }
            else
                Controller.Marker.gameObject.SetActive(false);
        }
    }


    #endregion
    #region Casting
    private void Casting()
    {
        castAnimation.SetBool("casting", true);
        Controller.Reel.ReelWorking(true, Vector3.back, Vector3.left);
    }
    #endregion

    private void Waiting()
    {
        
        Controller.Marker.transform.position = Controller.Bobber.transform.position;
        StartCoroutine(Fish());
    }

    IEnumerator Fish()
    {
        if (timerToBite >= timeForBite)
            {
                UpdateState(PlayerState.Biting);
                timerToBite = 0f;
            }
         timerToBite += Time.deltaTime;
         yield return null; // return within the while-loop       
    }

    void Catching()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Controller.Marker.gameObject.SetActive(false);
            Controller.Bending.Bending(true);
            Controller.Reel.ReelWorking(true, Vector3.forward, Vector3.right);
            Controller.Bobber.Pull();
        }
        else
        {
            Controller.Bending.Bending(false);
            Controller.Marker.transform.position = Controller.Bobber.transform.position;
            Controller.Marker.gameObject.SetActive(true);
        }
        castAnimation.SetBool("casting", false);
    }
}
