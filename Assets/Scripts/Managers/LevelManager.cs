using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // levels
    private List<Level> levels;
    private Level currentLevel;
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void toNextLevel()
    {
        print("in the future this will actually get the next level :)");
        restartLevel();
    }

    private void restartLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

    private void Start()
    {
        Controller.resultsController.onScoreboardOK += new onScoreboardOKEventHandler(toNextLevel);
        PlayerManager.instance.onDeathAnimationFinish += new onDeathAnimationFinishEventHandler(restartLevel);
        Controller.deathController.onRestartEarly += new onRestartEarlyEventHandler(restartLevel);
    }
}
