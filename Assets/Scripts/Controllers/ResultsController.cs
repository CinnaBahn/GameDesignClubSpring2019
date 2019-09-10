using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsController : Controller
{
    enum ResultsFSM
    {
        HIDDEN,
        STATS,
        NAME_INPUT,
        SCOREBOARD
    }

    private ResultsFSM state = ResultsFSM.HIDDEN;

    public Action onStatsOK, onNameInputOK, onScoreboardOK;
    public Action onLetterCycleNext, onLetterCyclePrev, onLetterSlotNext, onLetterSlotPrev;

    public GameObject scoreboardPrefab;
    private GameObject scoreboardWindow;

    private void statsOK(){ toState(ResultsFSM.STATS, ResultsFSM.NAME_INPUT); }
    private void nameInputOK(){ toState(ResultsFSM.NAME_INPUT, ResultsFSM.SCOREBOARD); }

    private bool upFired = false;
    private void upPressed()
    {
        if (state == ResultsFSM.NAME_INPUT)
            if (onLetterCycleNext != null)
                onLetterCycleNext();
    }

    private void downPressed()
    {
        if (state == ResultsFSM.NAME_INPUT)
            if (onLetterCyclePrev != null)
                onLetterCyclePrev();
    }

    private void leftPressed()
    {
        if (state == ResultsFSM.NAME_INPUT)
            if (onLetterSlotPrev != null)
                onLetterSlotPrev();
    }

    private void rightPressed()
    {
        if (state == ResultsFSM.NAME_INPUT)
            if (onLetterSlotNext != null)
                onLetterSlotNext();
    }

    private void primaryPressed()
    {
        if (state == ResultsFSM.STATS)
        {
            if (onStatsOK != null)
                onStatsOK();
        }
        else if (state == ResultsFSM.NAME_INPUT)
        {
            if (onNameInputOK != null)
                onNameInputOK();
        }
        else if (state == ResultsFSM.SCOREBOARD)
        {
            if (onScoreboardOK != null)
                onScoreboardOK();
        }
    }

    private void OnEnable()
    {
        toState(ResultsFSM.HIDDEN, ResultsFSM.STATS); state = ResultsFSM.STATS;


        // this class subscribes to its own events for consistency
        onStatsOK += statsOK;
        onNameInputOK += nameInputOK;

        InputManager.instance.onPrimaryPressed += primaryPressed;
        InputManager.instance.onUpPressed += upPressed;
        InputManager.instance.onDownPressed += downPressed;
        InputManager.instance.onLeftPressed += leftPressed;
        InputManager.instance.onRightPressed += rightPressed;
    }
    private void OnDisable()
    {
        onStatsOK -= statsOK;
        onNameInputOK -= nameInputOK;

        InputManager.instance.onPrimaryPressed -= primaryPressed;
        InputManager.instance.onUpPressed -= upPressed;
        InputManager.instance.onDownPressed -= downPressed;
        InputManager.instance.onLeftPressed -= leftPressed;
        InputManager.instance.onRightPressed -= rightPressed;
    }

    private void toState(ResultsFSM requiredFrom, ResultsFSM to)
    {
        if (state == requiredFrom)
            state = to;
    }

}
