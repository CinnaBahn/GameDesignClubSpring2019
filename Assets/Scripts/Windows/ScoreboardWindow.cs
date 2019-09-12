using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for handling input when the player is viewing the scoreboard after completing a level.
/// </summary>
public class ScoreboardWindow : Window
{
    private void OnEnable()
    {
        NameInputWindow n = this.GetComponent<NameInputWindow>();
        n.OnWindowDestroy += CreateWindow;
        n.OnWindowDestroy += EnableControls;
    }

    /// <inheritdoc/>
    public override void EnableControls()
    {
        InputManager i = InputManager.instance;
        i.onUpPressed += ScrollUp;
        i.onDownPressed += ScrollDown;

        i.onPrimaryPressed += DestroyWindow;
        i.onPrimaryPressed += DisableControls;
    }

    private void ScrollUp() { print("going up!"); }
    private void ScrollDown() { print("going down"); }

    /// <inheritdoc/>
    public override void DisableControls()
    {
        InputManager i = InputManager.instance;
        i.onUpPressed -= ScrollUp;
        i.onDownPressed -= ScrollDown;

        i.onPrimaryPressed -= DestroyWindow;
        i.onPrimaryPressed -= DisableControls;
    }

    private void OnDisable()
    {
        NameInputWindow n = this.GetComponent<NameInputWindow>();
        n.OnWindowDestroy -= CreateWindow;
        n.OnWindowDestroy -= EnableControls;
    }
}
