using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is a parent class. Controllers are responsible ONLY for taking button input and calling corresponding events.
// a Controller should not know who receives these events; other classes must subscribe to Controllers
// static references to the 1 and only Controller for each input type is included
public class Controller : MonoBehaviour
{
    public static TitleController titleController;
    public static CinematicController cinematicController;
    public static GameplayController gameplayController;
    public static DeathController deathController;
    public static ResultsController resultsController;

    private void Awake()
    {
        titleController = FindObjectOfType<TitleController>();
        cinematicController = FindObjectOfType<CinematicController>();
        gameplayController = FindObjectOfType<GameplayController>();
        deathController = FindObjectOfType<DeathController>();
        resultsController = FindObjectOfType<ResultsController>();
    }


    //protected int getVerticalInput() { return Input.GetAxis("Vertical").CompareTo(0); }
    //protected int getHorizontalInput() { return Input.GetAxis("Horizontal").CompareTo(0); }
}
