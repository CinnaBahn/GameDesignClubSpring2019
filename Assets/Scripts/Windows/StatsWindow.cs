using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsWindow : Window
{
    private void Start()
    {
        Goal.instance.onGoalReached += createWindow;
        Controller.resultsController.onStatsOK += destroyWindow;
    }

    protected override void createWindow()
    {
        base.createWindow();

        getTextComponent("Time").text = StatsManager.instance.getTime().ToString();
    }
}
