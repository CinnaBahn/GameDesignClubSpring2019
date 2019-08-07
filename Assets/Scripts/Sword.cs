using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float parryPower = 2000;

    private float duration = .25f;
    private bool swinging;

    public GameObject swingZonePrefab;
    private GameObject swingGO;
    private CircleCollider2D swingZone;

    private bool hit = false;

    public GameObject sparkParticlesPrefab;
    private ParticleSystem sparkParticles;

    private Rigidbody2D rb;

    private void Awake()
    {
        swingGO = GameObject.Instantiate<GameObject>(swingZonePrefab, transform);
        swingZone = swingGO.GetComponent<CircleCollider2D>();
        swingGO.SetActive(false);

        sparkParticles = GameObject.Instantiate<GameObject>(sparkParticlesPrefab, transform).GetComponent<ParticleSystem>();

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void swing()
    {
        if(!swinging)
            StartCoroutine( swingAnimation() );
    }

    private IEnumerator swingAnimation()
    {
        swingGO.SetActive(swinging = true);

        for(float i = 0; i < duration; i+=Time.deltaTime)
        {
            Collider2D[] overlap = new Collider2D[1]; //holds sword overlaps
            swingGO.transform.Rotate(Vector3.forward, i / duration * 360);

            ContactFilter2D c = new ContactFilter2D();
            c.SetLayerMask(1 << LayerMask.NameToLayer("Parry"));

            if (swingZone.OverlapCollider(c, overlap) > 0)
            {
                sparkParticles.Play();
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector3.Normalize(transform.position - overlap[0].transform.position) * parryPower);
                break;
            }
            yield return null;
        }

        swingGO.SetActive(swinging = false);

    }
}
