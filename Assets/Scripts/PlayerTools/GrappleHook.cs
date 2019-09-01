using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void onSuccessfulGrappleEventHandler(Grapplable hookedOn);
public delegate void onReleaseEventHandler(Grapplable releasedFrom);

public class GrappleHook : MonoBehaviour
{
    enum GrappleHookFSM
    {
        HOOKED,
        UNHOOKED
    }

    public event onSuccessfulGrappleEventHandler onSuccessfulGrapple;
    public event onReleaseEventHandler onRelease;

    public GameObject hookMasterPrefab;
    private HookPicker hookPicker;
    private LineRenderer lineRenderer;
    //private ConstantForce2D swingForce;
    private DistanceJoint2D ropeJoint;

    private GrappleHookFSM state = GrappleHookFSM.UNHOOKED;
    public float minRopeLength = 3;
    public float maxRopeLength = 35;
    public float contractSpeed = .15f;
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
        //GameplayController pc = GetComponent<GameplayController>();
        GameplayController pc = Controller.gameplayController;
        pc.onGrappleFired += new onGrappleFiredEventHandler(fire);
        pc.onGrappleReleased += new onGrappleReleasedEventHandler(release);
        pc.onGrappleContracted += new onGrappleContractedEventHandler(contract);
        pc.onGrappleLoosened += new onGrappleLoosenedEventHandler(loosen);
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
            if (!hookedOn)
            {
                release();
                break;
            }
            //draw
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, hookedOn.transform.position);
            yield return null;
        }
    }
}
