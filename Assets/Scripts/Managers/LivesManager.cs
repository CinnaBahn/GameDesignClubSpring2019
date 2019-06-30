using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public static int defaultLives = 3;
    private static int lives;

    void Start()
    {
        lives = defaultLives;
    }

    public static int loseLife() { return --lives; }
    public static int gainLife() { return ++lives; }
    public static void resetLives() { lives = defaultLives; }
}
