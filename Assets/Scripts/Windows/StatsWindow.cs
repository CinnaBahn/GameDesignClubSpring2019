using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class responsible for handling input when the level is just beaten by the player.
/// </summary>
public class StatsWindow : Window
{
    private void OnEnable()
    {
        Goal.instance.onGoalReached += CreateWindow;
        Goal.instance.onGoalReached += DisplayFinalTime;
        Goal.instance.onGoalReached += EnableControls;
    }

    private void DisplayFinalTime() { this.GetTextComponent("Time").text = StatsManager.instance.getTime().ToString(); }

    public override void EnableControls()
    {
        InputManager i = InputManager.instance;
        i.onPrimaryPressed += DestroyWindow;
        i.onPrimaryPressed += DisableControls;
    }

    public override void DisableControls()
    {
        InputManager i = InputManager.instance;
        i.onPrimaryPressed -= DisableControls;
        i.onPrimaryPressed -= DestroyWindow;
    }

    private void OnDisable()
    {
        Goal.instance.onGoalReached -= CreateWindow;
        Goal.instance.onGoalReached -= DisplayFinalTime;
        Goal.instance.onGoalReached -= EnableControls;
    }
}
