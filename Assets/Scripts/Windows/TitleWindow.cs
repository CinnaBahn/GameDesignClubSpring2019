using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleWindow : Window
{

    enum SelectionFSM
    {
        PLAY,
        CONTROLS,
        CREDITS
    }

    private SelectionFSM state = SelectionFSM.PLAY;
    public Action onPlay, onControls, onCredits;

    private List<Image> bgs;

    private bool ioValidOnEnabled = true; // if io not initialized yet, wait til Start to do it
    private void OnEnable()
    {
        InputManager io = InputManager.instance;
        if (io)
        {
            io.onPrimaryReleased += confirm;
            io.onUpPressed += rebouncePrevOption;
            io.onDownPressed += rebounceNextOption;
            io.onDirectionChanged += resetRebounce;
        }
        else
            ioValidOnEnabled = false;
    }

    private void OnDisable()
    {
        InputManager io = InputManager.instance;
        io.onPrimaryReleased -= confirm;
        io.onUpPressed -= rebouncePrevOption;
        io.onDownPressed -= rebounceNextOption;
        io.onDirectionChanged -= resetRebounce;
    }

    private void Start()
    {
        createWindow();
        bgs = new List<Image>(window.transform.GetComponentsInChildren<Image>());

        // if InputManager hasn't initialized yet, do it here in the Start function
        if (!ioValidOnEnabled)
            OnEnable();

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
                if (onPlay != null)
                    onPlay();
                break;
            case SelectionFSM.CONTROLS:
                if (onControls != null)
                    onControls();
                break;
            case SelectionFSM.CREDITS:
                if (onCredits != null)
                    onCredits();
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
