using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    //public parameters
    public bool hooked { get; set; }
    public float minRopeLength = 3;
    public float maxRopeLength = 35;
    public float contractSpeed = .15f;
    public float loosenSpeed = .15f;

    //components
    private LineRenderer lineRenderer;
    private ConstantForce2D swingForce;
    private DistanceJoint2D ropeJoint;
    private Grapplable grapplable;
    private HookFinder hookFinder;

    private GameObject hookedGO;
    //private GameObject oldBest;

    private void swing(EDirection dir) { swingForce.force = Direction.getDirectionVector(dir) * 10; }
    public void swingRight() { swing(EDirection.RIGHT); }
    public void swingLeft() { swing(EDirection.LEFT); }
    public void resetSwing() { swingForce.force = Vector2.zero; }

    void Start()
    {

        ropeJoint = gameObject.GetComponent<DistanceJoint2D>();

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        gameObject.GetComponent<LineRenderer>().positionCount = 2;
        
        swingForce = GetComponent<ConstantForce2D>();
        hookFinder = GetComponent<HookFinder>();
    }

    public void fire( EDirection dir )
    {
        Collider2D hit = hookFinder.getBestHook( dir );
        if (hit) {
            //variables
            hookedGO = hit.gameObject;
            grapplable = hookedGO.GetComponent<Grapplable>();
            hooked = grapplable.grapple();

            if (!hooked)
                return;

            //enable the rope physics
            ropeJoint.distance = Vector2.Distance(transform.position, hookedGO.transform.position);
            ropeJoint.connectedBody = hookedGO.GetComponent<Rigidbody2D>();
            ropeJoint.enabled = true;
            
            //draw the rope
            lineRenderer.enabled = true;
            StartCoroutine(holdOn());

            //check and call grapple events
            Event target = hookedGO.GetComponent<Event>();
            if (target)
                if (target.type == EEventType.ON_GRAPPLE)
                    target.target.SendMessage(target.function);
            //MAKE IT HANDLE MULTIPLE EVENTS ON 1 GO
        }
    }

    public void release()
    {
        hooked = ropeJoint.enabled = lineRenderer.enabled = false;
        StopCoroutine(holdOn());
        if (hookedGO)
        {
            //check and call de-grapple events
            Event target = hookedGO.GetComponent<Event>();
            if (target)
                if(target.type == EEventType.ON_RELEASE)
                    target.target.SendMessage(target.function);
            grapplable.release();
            hookedGO = null;
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
            if (!hookedGO)
            {
                release();
                break;
            }
            //draw
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, hookedGO.transform.position);
            yield return null;
        }
    }
}
