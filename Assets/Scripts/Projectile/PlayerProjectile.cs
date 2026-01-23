using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float speed;
    private float projectileDamage;
    private float travelDistance;
    private float xStartPos;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;
    private Rigidbody2D rb;

    private bool isGravityOn;
    private bool hasHit = false;
    private bool hasHitTarget;

    [SerializeField]
    private Transform damagePosition;
    private AudioSource arrowAudio;
    public AudioClip fleshSound;
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        arrowAudio = GetComponent<AudioSource>();
        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        isGravityOn = false;

        xStartPos = transform.position.x;
    }

    private void Update()
    {
        if (!hasHit)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void FixedUpdate()
    {

        if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
        {
            Destroy(gameObject);
            // isGravityOn = true;
            // rb.gravityScale = gravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Damageable"))
        {
            hasHitTarget = true;
            arrowAudio.PlayOneShot(fleshSound);
            HurtEnemyOnCollision(collision);
            Destroy(gameObject);
            // StickToCollider(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StickToCollider(collision);
        }

    }

    private void HurtEnemyOnCollision(Collider2D collision)
    {
        if (collision)
        {
            Debug.Log("Hit " + LayerMask.LayerToName(collision.gameObject.layer) + " " + collision.gameObject.name);
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                // Debug.Log("Damagable");
                damageable.DamageProjectile(projectileDamage);
            }
        }
    }

    private void StickToCollider(Collider2D collision)
    {
        hasHit = true;
        GetComponent<BoxCollider2D>().enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (hasHitTarget)
        {
            transform.parent = collision.transform;
        }
        rb.gravityScale = 0.0f;
        isGravityOn = false;

        Destroy(gameObject, Random.Range(1, 3));
    }

    public void FireProjectile(float speed, float travelDistance, float projectileDamage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        this.projectileDamage = projectileDamage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
