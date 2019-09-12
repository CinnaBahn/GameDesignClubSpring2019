using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour, IControllable
{
    public static GameplayManager instance;
    //public bool controlEnabled = false;
    private float timeOfControlEnabled = -1;

    // events of this class
    //public Action<ECauseOfDeath> onDeath;
    public Action onDeath;

    void Awake()
    {
        instance = this;
    }

    public void EnableControls()
    {
        GameObject p = PlayerManager.instance.getPlayer();
        p.GetComponent<GrappleHook>().enabled = p.GetComponent<Sword>().enabled = p.GetComponent<PlayerMovement>().enabled = true;
        timeOfControlEnabled = Time.time;
    }

    public void DisableControls()
    {
        GameObject p = PlayerManager.instance.getPlayer();
        p.GetComponent<GrappleHook>().enabled = p.GetComponent<Sword>().enabled = p.GetComponent<PlayerMovement>().enabled = false;
    }

    private void OnEnable()
    {
        Countdown.instance.onCountdownReached += EnableControls;
        onDeath += DisableControls;
        Goal.instance.onGoalReached += DisableControls;
        //Goal.instance.onGoalReached += goalReached;
    }
    private void OnDisable()
    {
        Countdown.instance.onCountdownReached -= EnableControls;
        onDeath -= DisableControls;
        Goal.instance.onGoalReached -= DisableControls;
        DisableControls();
        //Goal.instance.onGoalReached -= goalReached;
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
            onDeath();
            //onDeath(cause);
    }

    public void gameOver()
    {
        print("GAME OVER!!!!!!!!!!!");
    }

}
