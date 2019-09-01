using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardWindow : Window
{
    private void Start()
    {
        Controller.resultsController.onNameInputOK += new onNameInputOKEventHandler(createWindow);
        Controller.resultsController.onScoreboardOK += new onScoreboardOKEventHandler(destroyWindow);
    }
}
