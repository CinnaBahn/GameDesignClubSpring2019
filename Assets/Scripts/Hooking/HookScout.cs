using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class makes a list of all hookable hooks and highlights them
public class HookScout : MonoBehaviour
{
    private GameObject player;
    public List<Grapplable> hooks;
    private List<Grapplable> nearButUngrapplableHooks; // these are for hooks that enter the HookScout, but they are either obstructed or ungrapplable. keep them in a list in case something changes
    private List<Grapplable> toRemoveFromHooks; // these are hooks that were either destroyed or are no longer in line of sight
    private List<Grapplable> toRemoveFromNearBut; // these are hooks that were either destroyed or are now within line of sight

    private void Awake()
    {
        hooks = new List<Grapplable>();
        nearButUngrapplableHooks = new List<Grapplable>();
        toRemoveFromHooks = new List<Grapplable>();
        toRemoveFromNearBut = new List<Grapplable>();
    }

    private void Start()
    {
        player = PlayerManager.instance.getPlayer();
        GetComponent<CircleCollider2D>().radius = player.GetComponent<GrappleHook>().maxRopeLength / 2;
    }

    private void Update()
    {
        transform.position = player.transform.position;
        foreach(Grapplable g in nearButUngrapplableHooks)
        {
            if (!g) // if hook destroyed, mark for removal from list
                toRemoveFromNearBut.Add(g);
            else if (isSuitableToGrapple(g)) // if ungrapplable hook now grapplable...
            {
                addGrappableHook(g);
                toRemoveFromNearBut.Add(g);
            }
        }

        foreach (Grapplable g in hooks)
        {
            if (!g)
                toRemoveFromHooks.Add(g);
            else if (!isSuitableToGrapple(g)) // if grapplable hook now ungrapplable...
            {
                g.dehighlight();
                toRemoveFromNearBut.Add(g);
            }
        }

        // cleanup toRemove
        cleanupList(hooks, toRemoveFromHooks);
        cleanupList(nearButUngrapplableHooks, toRemoveFromNearBut);
    }

    private void cleanupList(List<Grapplable> original, List<Grapplable> toRemove)
    {
        foreach (Grapplable r in toRemove)
        {
            original.Remove(r);
        }
        toRemove.Clear();
    }

    private bool isSuitableToGrapple(Grapplable g)
    {
        return g.isGrapplable() && inLineOfSight(g.transform);
    }

    private bool inLineOfSight(Transform target)
    {
        int lm =~ LayerMask.GetMask("Player");
        RaycastHit2D r = Physics2D.Raycast(transform.position, target.position - transform.position, Vector2.Distance(target.position, transform.position), lm);
        return r.collider.transform == target;
    }

    private void addGrappableHook(Grapplable g)
    {
        hooks.Add(g);
        g.highlight();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Grapplable g = collision.GetComponent<Grapplable>();
        if (isSuitableToGrapple(g))
            addGrappableHook(g);
        else if(g.isGrapplable() && !inLineOfSight(collision.transform))
            nearButUngrapplableHooks.Add(g);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Grapplable g = collision.GetComponent<Grapplable>();
        if (g.isGrapplable())
        {
            hooks.Remove(g);
            nearButUngrapplableHooks.Remove(g);
            g.dehighlight();
        }
    }
}
