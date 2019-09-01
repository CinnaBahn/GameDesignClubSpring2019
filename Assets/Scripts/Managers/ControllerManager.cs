using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class keeps track of which controller is active; in other words, which controller gets to button input
public class ControllerManager : MonoBehaviour
{

    public enum ControllerFSM
    {
        TITLE,
        CINEMATIC,
        GAMEPLAY,
        DEATH,
        RESULTS
    }

    public static ControllerManager instance;
    public ControllerFSM state;
    private Controller currentController;
    private GameObject controllerGO;

    Dictionary<ControllerFSM, Controller> cd = new Dictionary<ControllerFSM, Controller>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //cd.Add(ControllerFSM.TITLE, titleC);
        //cd.Add(ControllerFSM.CINEMATIC, cinematicC);
        cd.Add(ControllerFSM.GAMEPLAY, Controller.gameplayController);
        cd.Add(ControllerFSM.DEATH, Controller.deathController);
        cd.Add(ControllerFSM.RESULTS, Controller.resultsController);

        //changeControllers(ControllerFSM.TITLE);

        foreach (Controller c in cd.Values)
        {
            c.active = false;
            //print(c.name);
        }

        Countdown.instance.onCountdownReached += new onCountdownReachedEventHandler(toGameplay);
        Goal.instance.onGoalReached += new onGoalReachedEventHandler(toResults);
        GameplayManager.instance.onDeath += new onDeathEventHandler(toDeath);
        //Controller.resultsController.onScoreboardOK += new onScoreboardOK()
    }

    public void toTitle() { changeControllers(ControllerFSM.TITLE); }
    public void toCinematic(){ changeControllers(ControllerFSM.CINEMATIC); }
    public void toGameplay() { changeControllers(ControllerFSM.GAMEPLAY); }
    public void toDeath(ECauseOfDeath cause) { changeControllers(ControllerFSM.DEATH); }
    public void toResults() { changeControllers(ControllerFSM.RESULTS); }

    public void changeControllers(ControllerFSM s)
    {
        if (currentController)
            currentController.active = false;

        state = s;
        currentController = cd[state];

        currentController.active = true;
    }
}
