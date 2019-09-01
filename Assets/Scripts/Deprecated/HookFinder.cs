using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookFinder : MonoBehaviour
{
    //hooks and stuff
    private GameObject best = null;
    private float diameter;
    private Color bestOriginalColor;
    private CircleCollider2D hookableZone;
    //prefabs to instantiate
    public GameObject hookableZonePrefab;

    private void Awake()
    {
        //hookable zone
        hookableZone = Instantiate<GameObject>(hookableZonePrefab).GetComponent<CircleCollider2D>();
        diameter = gameObject.GetComponent<GrappleHook>().maxRopeLength;
        hookableZone.radius = diameter / 2;
    }

    //Returns an array of Collider2Ds of GameObjects within maxRopeLength
    private Collider2D[] getHookables()
    {
        Collider2D[] hookables = new Collider2D[5]; //make sure to not clump hooks together!
        hookableZone.transform.position = gameObject.transform.position;
        ContactFilter2D c = new ContactFilter2D();
        c.SetLayerMask(1 << LayerMask.NameToLayer("Hook"));
        hookableZone.OverlapCollider(c, hookables);
        return hookables;
    }

    //Assigns a float value based on the direction we're aiming
    private float rateHook(Collider2D hook, EDirection aimDir)
    {
        return 1 / Vector2.Distance(new Vector2(transform.position.x, transform.position.y)
                                    + Direction.getDirectionVector(aimDir) * diameter * .75f,
                                    hook.transform.position);
    }

    //returns whether there in an object between the player and hook
    private bool hookObstructed(Collider2D targetHook) { return Physics2D.Raycast(transform.position, targetHook.transform.position - transform.position, Vector2.Distance(targetHook.transform.position, transform.position), LayerMask.NameToLayer("Hook")).collider; }

    //Returns the best hook (Collider2D) for a particular aiming direction
    public Collider2D getBestHook(EDirection aimDir)
    {
        Collider2D bestHook = null;
        float bestRating = 0;
        foreach (Collider2D currentHook in getHookables())
        {
            if (!currentHook) //make sure not null
                continue;

            if (hookObstructed(currentHook)) //make sure in line of sight
                continue;

            float currentRating = rateHook(currentHook, aimDir);
            if (currentRating > bestRating)
            {
                bestHook = currentHook;
                bestRating = currentRating;
            }
        }
        return bestHook;
    }

    //Turn the hook being aimed at red
    public void highlightBestHook(EDirection aimDir)
    {
        Collider2D c = getBestHook(aimDir);
        if (!c) //if new best is null, skip
            return;
        GameObject newBest = c.gameObject;
        if (!newBest.Equals(best))
        {
            //TODO FIX!!!!!!!!!!!!!!
            if (best) //if last best wasn't null
                best.GetComponent<SpriteRenderer>().color = bestOriginalColor;
            bestOriginalColor = newBest.GetComponent<SpriteRenderer>().color;
            newBest.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            best = newBest;
        }
    }
}
