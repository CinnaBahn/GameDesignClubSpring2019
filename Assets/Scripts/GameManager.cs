using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//This is the game manager. Due to it not being deleted on load it should only be created on the very first scene.
public class GameManager : MonoBehaviour
{
    public Text livesText = null;
    public Text timeText = null;
    
    public static GameManager instance;
    public static Scene gameOverScene;

    public static float time = 0;   //The time that is incremented.
    public static int score = 0;    //Holds how many points the player has scored.
    public static int lives = 3;    //Holds the number of lives the player has.


    private void Awake()
    {
        MakeSingleton();
        gameOverScene = SceneManager.GetActiveScene(); //For now GameOver is just going to restart the scene.

        //Initializes the text in the text fields.
        timeText.text = "Time: " + time.ToString();
        livesText.text = "Lives: " + lives.ToString();
     }
    
    //Called every frame.
    private void FixedUpdate()
    {
        Tick();
    }

    //Makes the object a singleton. This means there can only be one at a time and it doesn't destroy itself on load.
    private void MakeSingleton()
    {
        //If we already have a game manager, destroys this one.
        if (instance != null)
        {
            Destroy(gameObject);
        }
        //Otherwise, instance is set as equal to this instance, and it is marked not to be destroyed on load.
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    //If the player dies, the scene is restarted and lives are decremented.
    public void PlayerDeath()
    {
        Debug.Log("DEAD");
		//Reduces score and lives.
        score = IncrementScore(-100);
        lives = IncrementLives(-1);

        //If lives are less than 0 intitiates the game over seqeuence.
        /*
        if (lives < 0)
        {
            GameOver();
        }
        //Restarts the scene.
        else
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        */
    }

    //Initiates the game over sequence. For now just restarts the game. 
    public static void GameOver()
    {
        time = 0;
        score = 0;
        lives = 3;
        SceneManager.LoadScene(gameOverScene.name);
    }

    //Adds the passed value to that of lives, then returns the sum.
    public int IncrementLives(int increment)
    {
        livesText.text = "Lives: " + lives.ToString();
        return lives + increment;
    }

    //Adds the passed value and the score, then returns the sum. Minimum of 0.
    public int IncrementScore(int increment)
    {
        return Math.Max(score + increment, 0);
    }

    //Increments the timer.
    public void Tick()
    {
        time += Time.deltaTime;
        timeText.text = "Time: " + time.ToString();
    }
}
