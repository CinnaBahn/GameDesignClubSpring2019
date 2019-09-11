using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : Controller
{

    enum TitleFSM
    {
        TITLE,
        CONTROLS,
        CREDITS
    }

    private TitleFSM state = TitleFSM.TITLE;

    public Action onPlaySelect, onControlsSelect, onCreditsSelect;
    public Action onControlsOK, onCreditsOK;
    public Action onNextOption, onPrevOption;
    //public Action onPrimary


    // primary
    private void primaryReleased()
    {
        /*
        switch (currentWindow)
        {
            case TitleFSM.TITLE:
                break;
            case TitleFSM.CONTROLS:
                break;
            case TitleFSM.CREDITS:
                break;
        }
        */

    }

    // dpad
    /*
    private void upPressed()
    {
        if (currentWindow == TitleFSM.TITLE)
            if (onNextOption != null)
                onNextOption();
    }

    private void downPressed()
    {
        if(currentWindow == TitleFSM.TITLE)
            if (onPrevOption != null)
                onPrevOption();
    }
    */
}
