using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // levels
    //private List<Level> levels;
    public static int currentLevelIndex = 0;
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
        //SceneManager.LoadScene(currentLevelIndex);
    }

    private void toNextLevel()
    {
        //print("in the future this will actually get the next level :)");
        SceneManager.LoadScene(++currentLevelIndex);
        //restartLevel();
    }

    private void restartLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

    private void Start()
    {
        Controller.resultsController.onScoreboardOK += toNextLevel;
        PlayerManager.instance.onDeathAnimationFinish += restartLevel;
        Controller.deathController.onRestartEarly += restartLevel;
    }
}
