using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class picks the best hook from HookScout's list and indicates it with a graphical line
public class HookPicker : MonoBehaviour
{
    private List<Grapplable> hooks;
    private Grapplable bestHook;
    private float idealDistance;
    private LineRenderer lr;

    private void Awake()
    {
        lr = gameObject.GetComponent<LineRenderer>();
    }

    private void Start()
    {
        hooks = GetComponent<HookScout>().hooks;
        idealDistance = PlayerManager.instance.getPlayer().GetComponent<GrappleHook>().maxRopeLength * .75f;
    }

    private void Update()
    {
        bestHook = pickBestHook();
        drawLineToBestHook();
    }

    public Grapplable getBestHook()
    {
        return bestHook;
    }

    private Grapplable pickBestHook()
    {
        Grapplable bestSoFar = null;
        float bestRatingSoFar = 0;
        foreach (Grapplable currentHook in hooks)
        {
            float currentRating = rateHook(currentHook);
            if (currentRating > bestRatingSoFar)
            {
                bestSoFar = currentHook;
                bestRatingSoFar = currentRating;
            }
        }
        return bestSoFar;
    }

    // assign a float value based on the direction we're aiming
    private float rateHook(Grapplable hook)
    {
        return 1 / Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
                                    + Direction.getAimingVector() * idealDistance,
                                    hook.transform.position);
    }

    private void drawLineToBestHook()
    {
        if (bestHook)
        {
            if (!lr.enabled)
                lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, bestHook.transform.position);
        }
        else if (lr.enabled)
            lr.enabled = false;
    }
}
