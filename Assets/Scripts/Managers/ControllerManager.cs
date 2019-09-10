using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class keeps track of which controller is active; in other words, which controller gets to button input
public class ControllerManager : MonoBehaviour
{
    public static ControllerManager instance;

    private Controller currentController;
    private GameObject controllerGO;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        //changeControllers(ControllerFSM.TITLE);

        foreach (Controller c in FindObjectsOfType<Controller>())
            c.enabled = false;

        Countdown.instance.onCountdownReached += toGameplay;
        Goal.instance.onGoalReached += toResults;
        GameplayManager.instance.onDeath += toDeath;
        //Controller.resultsController.onScoreboardOK += new onScoreboardOK()
    }

    public void toTitle() { changeControllers(Controller.titleController); }
    public void toCinematic(){ changeControllers(Controller.cinematicController); }
    public void toGameplay() { changeControllers(Controller.gameplayController); }
    public void toDeath(ECauseOfDeath cause) { changeControllers(Controller.deathController); }
    public void toResults() { changeControllers(Controller.resultsController); }

    public void changeControllers(Controller c)
    {
        if (currentController)
            currentController.enabled = false;
        currentController = c;
        currentController.enabled = true;
    }
}
