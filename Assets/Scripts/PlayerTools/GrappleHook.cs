using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    enum GrappleHookFSM
    {
        HOOKED,
        UNHOOKED
    }

    /*
     * delegates that hold function to be called
     * the <Grapplable> means that these functions take a Grapplable object as a parameter
     * these return void
     */
    public Action<Grapplable> onSuccessfulGrapple;
    public Action whileGrappled;
    public Action<Grapplable> onRelease;

    public GameObject hookMasterPrefab;
    private HookPicker hookPicker;
    private LineRenderer lineRenderer;
    //private ConstantForce2D swingForce;
    private DistanceJoint2D ropeJoint;

    private GrappleHookFSM state = GrappleHookFSM.UNHOOKED;
    public float minRopeLength = 3;
    public float maxRopeLength = 35;
    public float contractSpeed = .2f;
    public float loosenSpeed = .15f;
    private Grapplable hookedOn;

    private void Awake()
    {
        ropeJoint = GetComponent<DistanceJoint2D>();

        lineRenderer = GetComponent<LineRenderer>();
        GetComponent<LineRenderer>().positionCount = 2;

        //swingForce = GetComponent<ConstantForce2D>();

        hookPicker = Instantiate<GameObject>(hookMasterPrefab).GetComponent<HookPicker>();
    }

    private void Start()
    {
        // events
        GameplayController pc = Controller.gameplayController;
        pc.onGrappleFired += fire;
        pc.onGrappleReleased += release;
        pc.onGrappleContracted += contract;
        pc.onGrappleLoosened += loosen;
    }

    public bool isHooked()
    {
        return state == GrappleHookFSM.HOOKED;
    }

    public void fire()
    {
        hookedOn = hookPicker.getBestHook();
        if (hookedOn) {

            if (hookedOn.grapple())
                state = GrappleHookFSM.HOOKED;
            if (state == GrappleHookFSM.UNHOOKED)
                return;

            if (onSuccessfulGrapple != null)
                onSuccessfulGrapple(hookedOn);

            //enable the rope physics
            ropeJoint.distance = Vector2.Distance(transform.position, hookedOn.transform.position);
            ropeJoint.connectedBody = hookedOn.GetComponent<Rigidbody2D>();
            ropeJoint.enabled = true;
            
            //draw the rope
            lineRenderer.enabled = true;
            StartCoroutine(holdOn());

            // get rid of this
            //check and call grapple events
            Event target = hookedOn.GetComponent<Event>();
            if (target)
                if (target.type == EEventType.ON_GRAPPLE)
                    target.target.SendMessage(target.function);
            //MAKE IT HANDLE MULTIPLE EVENTS ON 1 GO
        }
    }

    public void release()
    {
        state = GrappleHookFSM.UNHOOKED;
        ropeJoint.enabled = lineRenderer.enabled = false;
        if (onRelease != null)
            onRelease(hookedOn);

        StopCoroutine(holdOn());
        if (hookedOn)
        {
            //get rid of this
            //check and call de-grapple events
            Event target = hookedOn.GetComponent<Event>();
            if (target)
                if(target.type == EEventType.ON_RELEASE)
                    target.target.SendMessage(target.function);
            hookedOn.release();
            hookedOn = null;
        }
    }

    public void contract()
    {
        ropeJoint.distance = Mathf.Clamp(ropeJoint.distance - contractSpeed, minRopeLength, maxRopeLength / 2);
    }

    public void loosen()
    {
        ropeJoint.distance = Mathf.Clamp(ropeJoint.distance + loosenSpeed, minRopeLength, maxRopeLength / 2);
    }

    private IEnumerator holdOn()
    {
        while(true)
        {
            // check if what we're hooked onto still exists (in case it gets destroyed while we're swinging)
            if (!hookedOn)
            {
                release();
                break;
            }
            // draw
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, hookedOn.transform.position);
            yield return null;
        }
    }
}
