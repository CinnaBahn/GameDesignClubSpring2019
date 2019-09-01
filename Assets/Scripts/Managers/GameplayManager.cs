using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void onDeathEventHandler(ECauseOfDeath cause);

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    //public bool controlEnabled = false;
    private float timeOfControlEnabled = -1;

    public event onDeathEventHandler onDeath;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Countdown.instance.onCountdownReached += new onCountdownReachedEventHandler(startLevel);
        Goal.instance.onGoalReached += new onGoalReachedEventHandler(beatLevel);
    }

    private void startLevel()
    {
        //controlEnabled = true;
        timeOfControlEnabled = Time.time;
    }

    public void beatLevel()
    {

    }

    public void killPlayer(ECauseOfDeath cause)
    {
        /* reimplement when lives working again
        StatsManager.instance.loseLife();
        if (StatsManager.instance.getLives() == 0)
        {
            gameOver();
            StatsManager.instance.resetLives();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StatsManager.instance.resetTime();
        }
        */

        // call event for all listeners
        if (onDeath != null)
            onDeath(cause);
    }

    public void gameOver()
    {
        print("GAME OVER!!!!!!!!!!!");
    }

}
