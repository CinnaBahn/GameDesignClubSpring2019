using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public delegate void onFocusEventHandler(); // when the ResultsController is the current controller
public delegate void onStatsOKEventHandler();
public delegate void onNameInputOKEventHandler();
public delegate void onScoreboardOKEventHandler();
//public delegate void onUnfocusEventHandler(); // when the ResultsController is not the current controller

public delegate void onLetterCycleNextEventHandler();
public delegate void onLetterCyclePrevEventHandler();
public delegate void onLetterSlotNextEventHandler();
public delegate void onLetterSlotPrevEventHandler();

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
    private EDirection lastDpad = EDirection.CENTER;

    //public event onFocusEventHandler onFocus;
    public event onStatsOKEventHandler onStatsOK;
    public event onNameInputOKEventHandler onNameInputOK;
    public event onScoreboardOKEventHandler onScoreboardOK;
    //public event onUnfocusEventHandler onUnfocus;

    public event onLetterCycleNextEventHandler onLetterCycleNext;
    public event onLetterCyclePrevEventHandler onLetterCyclePrev;
    public event onLetterSlotNextEventHandler onLetterSlotNext;
    public event onLetterSlotPrevEventHandler onLetterSlotPrev;

    public GameObject scoreboardPrefab;
    private GameObject scoreboardWindow;

    private void Start()
    {
        Goal.instance.onGoalReached += new onGoalReachedEventHandler(toStats);
        
        onStatsOK += new onStatsOKEventHandler(toNameInput);
        onNameInputOK += new onNameInputOKEventHandler(toScoreboard);
        //onScoreboardOK += new onScoreboardOK(onResultsClose);
    }

    private void toState(ResultsFSM requiredFrom, ResultsFSM to)
    {
        if (state == requiredFrom)
            state = to;
    }

    private void toStats() { toState(ResultsFSM.HIDDEN, ResultsFSM.STATS); }
    private void toNameInput() { toState(ResultsFSM.STATS, ResultsFSM.NAME_INPUT); }
    private void toScoreboard() { toState(ResultsFSM.NAME_INPUT, ResultsFSM.SCOREBOARD); }

    private void Update()
    {
        if (active)
        {
            if (state == ResultsFSM.STATS)
            {
                if (Input.GetButtonDown("Fire1"))
                    onStatsOK();
            }
            else if (state == ResultsFSM.NAME_INPUT)
            {
                if (Input.GetButtonDown("Fire1"))
                    onNameInputOK();

                int vert = getVerticalInput();
                if (vert == 1 && lastDpad != EDirection.UP) // only trigger if this is the 1st frame pressing up, same for following event calls
                    onLetterCycleNext();
                else if (vert == -1 && lastDpad != EDirection.DOWN)
                    onLetterCyclePrev();

                int hor = getHorizontalInput();
                if (hor == 1 && lastDpad != EDirection.RIGHT)
                    onLetterSlotNext();
                else if (hor == -1 && lastDpad != EDirection.LEFT)
                    onLetterSlotPrev();
            }
            else if(state == ResultsFSM.SCOREBOARD)
            {
                if (Input.GetButtonDown("Fire1"))
                    onScoreboardOK();
            }
            lastDpad = Direction.getAimingDirection();
        }
    }


}
