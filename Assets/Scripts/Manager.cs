using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager manager;

    //make sure we only have 1 gameobject (don't duplicate it on reload/new level)
    void Awake()
    {
        if (manager)
            Destroy(gameObject);
        else
            manager = this;
    }

}
