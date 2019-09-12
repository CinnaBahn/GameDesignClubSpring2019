using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEar : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Countdown.instance.onCountdownReached += startListening;
        Goal.instance.onGoalReached += stopListening;
        //GameplayManager.instance.onDeath += disable;
        //Countdown.instance.onCountdownReached += gh.release;
    }

    private void startListening()
    {
        GrappleHook gh = PlayerManager.instance.GetComponent<GrappleHook>();
        InputManager i = InputManager.instance;

        //i.onPrimaryPressed += gh.yeet;
        //InputManager.instance.onPrimaryPressed += PlayerManager.instance.GetComponent<GrappleHook>().fire;
        //i.onPrimaryPressed += gh.fire;
        //i.onPrimaryReleased += gh.release;
    }

    private void stopListening()
    {
        GrappleHook gh = PlayerManager.instance.GetComponent<GrappleHook>();
        InputManager i = InputManager.instance;

        i.onPrimaryPressed -= gh.fire;
        i.onPrimaryReleased -= gh.release;
    }

}
