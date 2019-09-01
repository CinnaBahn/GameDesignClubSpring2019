using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : Controller
{
    public string demoLevel;

    private void Update()
    {
        if (active)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                SceneManager.LoadScene(demoLevel);
            }
        }
    }
}
