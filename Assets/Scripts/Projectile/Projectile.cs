using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
    {
        private float speed;
        private float travelDistance;
        private float xStartPos;

        [SerializeField]
        private float gravity;
        [SerializeField]
        private float damageRadius;
        [SerializeField]
        private float projectileDamage = 25f;
        private Rigidbody2D rb;

        private bool isGravityOn;
        private bool hasHit = false;
        private bool hasHitTarget;

        [SerializeField]
        private Transform damagePosition;

        private void Start()
        {

            rb = GetComponent<Rigidbody2D>();

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
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.gameObject.layer == LayerMask.NameToLayer("Combat"))
            {
                hasHitTarget = true;
                HurtPlayerOnCollision(collision);
                Destroy(gameObject);
                // StickToCollider(collision);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                StickToCollider(collision);
            }

        }

        private void HurtPlayerOnCollision(Collider2D collision)
        {
            if (collision)
            {
                Debug.Log("Hit " + LayerMask.LayerToName(collision.gameObject.layer) + " " + collision.gameObject.name);
                IDamageable damageable = collision.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    // Debug.Log("Damagable");
                    damageable.Damage(projectileDamage);
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

        public void FireProjectile(float speed, float travelDistance)
        {
            this.speed = speed;
            this.travelDistance = travelDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
        }
    }