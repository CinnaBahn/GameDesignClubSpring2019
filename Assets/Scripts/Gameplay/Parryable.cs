using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parryable : MonoBehaviour
{
    protected enum ParryFSM
    {
        PARRYABLE,
        UNPARRYABLE
    }

    protected ParryFSM state = ParryFSM.PARRYABLE;

    public virtual bool onParry(Transform initiator)
    {
        bool success = true;
        if (state == ParryFSM.PARRYABLE)
            bumpInitiator(initiator.GetComponent<Rigidbody2D>());
        else
            success = false;
        return success;
    }

    private void bumpInitiator(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
        Vector2 f = rb.transform.position - transform.position;
        f.Normalize();
        f *= 2000;
        rb.AddForce(f);
    }

    public bool parryable()
    {
        return state == ParryFSM.PARRYABLE;
    }
}
