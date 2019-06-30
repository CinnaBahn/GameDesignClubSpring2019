using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    void Awake()
    {
        instance = this;
    }

    public void gameOver()
    {
        print("GAME OVER!!!!!!!!!!!");
    }

}
