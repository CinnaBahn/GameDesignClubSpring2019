using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public int timeUntilControlEnabled = 0;
    public bool controlEnabled = false;
    public float timeOfControlEnabled = -1;

    void Awake()
    {
        instance = this;
        StartCoroutine(startCountdown());
    }

    public void gameOver()
    {
        print("GAME OVER!!!!!!!!!!!");
    }

    private IEnumerator startCountdown()
    {
        //print("yee");
        for (timeUntilControlEnabled = 3; timeUntilControlEnabled > 0; timeUntilControlEnabled--)
        {
            //print(timeUntilControlEnabled);
            yield return new WaitForSeconds(.5f);
        }

        controlEnabled = true;
        timeOfControlEnabled = Time.time;
    }

}
