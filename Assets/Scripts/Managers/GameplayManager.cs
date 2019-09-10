using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    //public bool controlEnabled = false;
    private float timeOfControlEnabled = -1;

    // events of this class
    public Action<ECauseOfDeath> onDeath;

    void Awake()
    {
        instance = this;
    }

    private void countdownReached(){ timeOfControlEnabled = Time.time; }
    private void goalReached(){ }

    private void OnEnable()
    {
        Countdown.instance.onCountdownReached += countdownReached;
        Goal.instance.onGoalReached += goalReached;
    }
    private void OnDisable()
    {
        Countdown.instance.onCountdownReached -= countdownReached;
        Goal.instance.onGoalReached -= goalReached;
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
