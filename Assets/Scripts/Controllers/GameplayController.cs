using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : Controller {

    enum GameplayFSM
    {
        HOOKED,
        FREE
    }

    private GameplayFSM state = GameplayFSM.FREE;

    void Start()
    {
        GrappleHook g = PlayerManager.instance.getPlayer().GetComponent<GrappleHook>();
        g.onSuccessfulGrapple += toHooked;
        g.onRelease += toFree;
    }

    private void toHooked(Grapplable hookedOn) { state = GameplayFSM.HOOKED; }
    private void toFree(Grapplable releasedFrom) { state = GameplayFSM.FREE; }
}
