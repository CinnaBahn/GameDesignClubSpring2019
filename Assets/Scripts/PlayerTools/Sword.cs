using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private readonly float duration = .25f;
    private bool spinning;

    public GameObject spinZonePrefab; // prefab to spawn
    private GameObject spinGO; // instantated gameobject
    private CircleCollider2D spinZone; // collider component on gameobject

    public GameObject sparkParticlesPrefab;
    private ParticleSystem sparkParticles;

    private void Awake()
    {
        spinGO = Instantiate<GameObject>(spinZonePrefab, transform);
        spinZone = spinGO.GetComponent<CircleCollider2D>();
        spinGO.SetActive(false);

        sparkParticles = Instantiate<GameObject>(sparkParticlesPrefab, transform).GetComponent<ParticleSystem>();
    }

    public void Start()
    {
        //GetComponent<GameplayController>().onParrySwing+= new onParrySwingEventHandler(spin);
        Controller.gameplayController.onParrySpin += new onParrySpinEventHandler(spin);
    }

    // called when spin button pressed
    public void spin()
    {
        if(!spinning)
            StartCoroutine( spinAnimation() );
    }

    // called when a Parry object successfully parried
    public void parrySuccessful(Parryable victim)
    {
        sparkParticles.Play();
    }

    // checks and parries all overlapping Parry objects until 1 parryable one found
    // currently only parries 1 thing, even if overlapping 2
    private Parryable testHit()
    {
        Collider2D[] overlap = new Collider2D[1]; // holds sword overlaps
        ContactFilter2D cf = new ContactFilter2D();
        cf.SetLayerMask(1 << LayerMask.NameToLayer("Parry"));

        spinZone.OverlapCollider(cf, overlap);

        foreach (Collider2D c in overlap)
            if (c)
            {
                Parryable p = c.GetComponent<Parryable>();
                if (p.onParry(transform))
                    return p;
            }
        return null; // didn't hit anything
    }

    private IEnumerator spinAnimation()
    {
        spinGO.SetActive(spinning = true);

        for(float i = 0; i < duration; i+=Time.deltaTime)
        {
            spinGO.transform.Rotate(Vector3.forward, i / duration * 360);
            Parryable victim = testHit();
            if (victim)
            {
                parrySuccessful(victim);
                break;
            }
            yield return null;
        }
        spinGO.SetActive(spinning = false);
    }
}
