using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsWindow : Window
{
    private void Start()
    {
        Goal.instance.onGoalReached += new onGoalReachedEventHandler(createWindow);
        Controller.resultsController.onStatsOK += new onStatsOKEventHandler(destroyWindow);
    }
}
