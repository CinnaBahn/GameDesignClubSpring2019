using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardWindow : Window
{
    private void Start()
    {
        Controller.resultsController.onNameInputOK += createWindow;
        Controller.resultsController.onScoreboardOK += destroyWindow;
    }
}
