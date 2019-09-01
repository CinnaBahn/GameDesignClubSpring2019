using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level
{
    string name;
    Scene scene;

    public Level(string n)
    {
        name = n;
    }

    public Level()
    {
        name = "need to give a name yo";
    }
}
