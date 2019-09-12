using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for handling input for the Title Menu when the game first loads.
/// </summary>
public class TitleWindow : Window
{

    private enum SelectionFSM
    {
        PLAY,
        CONTROLS,
        CREDITS,
    }

    private SelectionFSM state = SelectionFSM.PLAY;
    public Action OnPlay, OnControls, OnCredits;

    private List<Image> bgs;

    private void OnEnable()
    {

    }

    public override void EnableControls()
    {
        InputManager io = InputManager.instance;
        io.onPrimaryReleased += confirm;
        io.onUpPressed += rebouncePrevOption;
        io.onDownPressed += rebounceNextOption;
        io.onDirectionChanged += resetRebounce;
    }

    private void OnDisable()
    {

    }

    public override void DisableControls()
    {
        InputManager io = InputManager.instance;
        io.onPrimaryReleased -= confirm;
        io.onUpPressed -= rebouncePrevOption;
        io.onDownPressed -= rebounceNextOption;
        io.onDirectionChanged -= resetRebounce;
    }

    private void Start()
    {
        CreateWindow();
        bgs = new List<Image>(window.transform.GetComponentsInChildren<Image>());

        prevOption();
    }

    // when dpad direction is changed, stop rebouncing
    private void resetRebounce()
    {
        StopAllCoroutines();
        rebouncing = false;
    }

    // start rebouncing the next option button
    private void rebounceNextOption()
    {
        StartCoroutine(rebounce(EDirection.DOWN, false));
    }

    // start rebouncing the prev option button
    private void rebouncePrevOption()
    {
        StartCoroutine(rebounce(EDirection.UP, false));
    }

    // go to the next option once
    private void nextOption()
    {
        bgs[(int)state].color = Color.white;
        state = (SelectionFSM)Mathf.Min((int)state + 1, Enum.GetNames(typeof(SelectionFSM)).Length - 1);
        bgs[(int)state].color = Color.red;
        //print(state);
    }

    // go to the prev option once
    private void prevOption()
    {
        bgs[(int)state].color = Color.white;
        state = (SelectionFSM)Mathf.Max((int)state - 1, 0);
        bgs[(int)state].color = Color.red;
        //print(state);
    }

    private void confirm()
    {
        switch (state)
        {
            case SelectionFSM.PLAY:
                if (OnPlay != null)
                    OnPlay();
                break;
            case SelectionFSM.CONTROLS:
                if (OnControls != null)
                    OnControls();
                break;
            case SelectionFSM.CREDITS:
                if (OnCredits != null)
                    OnCredits();
                break;
        }
    }

    // a function that call prev/next option, and if the button is held down, will continue to be called periodically
    private bool rebouncing = false;
    private IEnumerator rebounce(EDirection dir, bool calledInternally)
    {

        // don't run if spam-called by InputManager
        if (rebouncing && !calledInternally)
            yield break;
        rebouncing = true;

        if (dir == EDirection.UP)
            prevOption();
        else if (dir == EDirection.DOWN)
            nextOption();

        for (float i = 0; i < .5f; i += Time.deltaTime)
            yield return null;

        // rebounce if still held in same dir, otherwise stop
        if (Direction.getAimingDirection() == dir)
            StartCoroutine(rebounce(dir, true));
        else
            rebouncing = false;
    }
}
