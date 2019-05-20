using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    //public parameters
    public bool swinging { get; set; }
    public float minRopeLength = 3;
    public float maxRopeLength = 35;
    public float contractSpeed = .15f;
    public float loosenSpeed = .15f;

    //prefabs to instantiate
    public GameObject hookableZonePrefab;

    //components
    private LineRenderer lineRenderer;
    private CircleCollider2D hookableZone;
    private ConstantForce2D swingForce;
    private DistanceJoint2D ropeJoint;

    //hooks and stuff
    private GameObject best = null;
    private Color bestOriginalColor;
    private GameObject hookedGO;
    //private GameObject oldBest;

    void setupComponents()
    {
        //rope joint
        ropeJoint = gameObject.GetComponent<DistanceJoint2D>();
        //hookable zone
        hookableZone = Instantiate<GameObject>(hookableZonePrefab).GetComponent<CircleCollider2D>();
        hookableZone.radius = maxRopeLength / 2;
        //line renderer
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        gameObject.GetComponent<LineRenderer>().positionCount = 2;
        //swing force
        swingForce = GetComponent<ConstantForce2D>();
    }

    private void swing(EDirection dir) { swingForce.force = Direction.getDirectionVector(dir) * 10; }
    public void swingRight() { swing(EDirection.RIGHT); }
    public void swingLeft() { swing(EDirection.LEFT); }
    public void resetSwing() { swingForce.force = Vector2.zero; }

    void Start()
    {
        setupComponents();
    }

    private void Update()
    {
        hookableZone.transform.position = gameObject.transform.position;
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
    private float rateHook( Collider2D hook, EDirection aimDir)
    {
        float distRating = 1 / Vector2.Distance( new Vector2(transform.position.x, transform.position.y)
                                                    + Direction.getDirectionVector(aimDir) * maxRopeLength * .75f,
                                                    hook.transform.position);
        return distRating;
    }

    //Returns the best hook (Collider2D) for a particular aiming direction
    private Collider2D getBestHook( EDirection aimDir )
    {
        Collider2D bestHook = null;
        float bestRating = 0;
        foreach(Collider2D currentHook in getHookables())
        {
            if (!currentHook)
                continue;

            float currentRating = rateHook(currentHook, aimDir);
            if(currentRating > bestRating)
            {
                bestHook = currentHook;
                bestRating = currentRating;
            }
        }
        return bestHook;
    }

    //Turn the hook being aimed at red
    public void highlightBestHook( EDirection aimDir )
    {
        Collider2D c = getBestHook(aimDir);
        if (!c) //if new best is null, skip
            return;
        GameObject newBest = c.gameObject;
        if(!newBest.Equals(best))
        {
            //TODO FIX!!!!!!!!!!!!!!
            if (best) //if last best wasn't null
                best.GetComponent<SpriteRenderer>().color = bestOriginalColor;
            bestOriginalColor = newBest.GetComponent<SpriteRenderer>().color;
            newBest.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            best = newBest;
        }
    }

    public void fire( EDirection dir )
    {
        Collider2D hit = getBestHook( dir );
        if (hit) {
            //variables
            swinging = true;
            hookedGO = hit.gameObject;

            //enable the rope physics
            ropeJoint.distance = Vector2.Distance(transform.position, hookedGO.transform.position);
            ropeJoint.connectedBody = hookedGO.GetComponent<Rigidbody2D>();
            ropeJoint.enabled = true;
            
            //draw the rope
            lineRenderer.enabled = true;
            StartCoroutine("drawRope");

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
        if (hookedGO)
        {
            swinging = ropeJoint.enabled = lineRenderer.enabled = false;
            //check and call de-grapple events
            Event target = hookedGO.GetComponent<Event>();
            if (target)
                if(target.type == EEventType.ON_RELEASE)
                    target.target.SendMessage(target.function);
            hookedGO = null;
            StopCoroutine("drawRope");
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

    private IEnumerator drawRope()
    {
        while(true)
        {
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, hookedGO.transform.position);
            yield return null;
        }
    }
}
