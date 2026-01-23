using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField] private GameObject damageParticles;
    private Movement Movement {get => movement ??= core.GetCoreComponent<Movement>();}
    private CollisionSenses CollisionSenses {get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>();}
    private Movement movement;
    private CollisionSenses collisionSenses;
    private Stats Stats {get => stats ??= core.GetCoreComponent<Stats>();}
    private Stats stats;
    private ParticleManager ParticleManager {get => particleManager ?? core.GetCoreComponent(ref particleManager);}
    private ParticleManager particleManager;
    private int projectileLayer;
    private SpriteRenderer playerSprite;
    public AudioSource playerAudioSource;
    public AudioClip playerHurtSound;
    [SerializeField] private float maxKnockbackTime = 0.25f;
    private bool isKnockbackActive;
    private float KnockbackStartTime;
    public bool isImmune;
    private float ImmuneStartTime;
    private float ImmmuneDuration = 2f;
    private Color flashColor = new Color(1f, 1f, 1f, 135/255);
    protected override void Awake()
    {
        base.Awake();
        isImmune = false;
        playerSprite = core.GetComponentInParent<SpriteRenderer>();
        playerAudioSource = GetComponent<AudioSource>();
        projectileLayer = LayerMask.NameToLayer("Projectile");
    }
    public override void LogicUpdate()
    {
        CheckKnockback();
        CheckImmune();
    }
    public void Damage(float amount)
    {
        if (transform.gameObject.layer == LayerMask.NameToLayer("Combat") && !isImmune)
        {
            Stats?.DecreaseHealth(amount);
            Debug.Log(core.transform.parent.name + " Damaged!");
            ParticleManager?.StartParticlesRandomRotation(damageParticles);
            isImmune = true;
            ImmuneStartTime = Time.time;
            Physics2D.IgnoreLayerCollision(projectileLayer, gameObject.layer, true);
            // playerAudioSource.clip = playerHurtSound;
            // playerAudioSource.loop = false;
            playerAudioSource.PlayOneShot(playerHurtSound);
            // Debug.Log(ImmuneStartTime);
            if (Stats.currentHealth > 0) {
                StartCoroutine(FlashCoroutine(ImmmuneDuration, flashColor, 8));
            }
        }
        else if (transform.gameObject.layer == LayerMask.NameToLayer("Damageable"))
        {
            Stats?.DecreaseHealth(amount);
            Debug.Log(core.transform.parent.name + " Damaged!");
            ParticleManager?.StartParticlesRandomRotation(damageParticles);
        }
    }
    public void DamageProjectile(float amount)
    {
        Stats?.DecreaseHealthWithoutStun(amount);
        Debug.Log(core.transform.parent.name + " Damaged!");
        ParticleManager?.StartParticlesRandomRotation(damageParticles);
    }
    public void Knockback(float strength, Vector2 angle, int direction)
    {
        if (transform.gameObject.layer == LayerMask.NameToLayer("Combat") && !isImmune)
        {
            Movement?.SetVelocity(strength, angle, direction);
            Movement.CanSetVelocity = false;
            isKnockbackActive = true;
            KnockbackStartTime = Time.time;
        }
        else if (transform.gameObject.layer == LayerMask.NameToLayer("Damageable"))
        {
            Movement?.SetVelocity(strength, angle, direction);
            Movement.CanSetVelocity = false;
            isKnockbackActive = true;
            KnockbackStartTime = Time.time;
        }
    }
    private void CheckKnockback()
    {
        if (isKnockbackActive && ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.Grounded) || Time.time >= KnockbackStartTime + maxKnockbackTime))
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
    private void CheckImmune()
    {
        // Debug.Log("Immune = " + isImmune);
        if (isImmune && Time.time >= ImmuneStartTime + ImmmuneDuration)
        {
            isImmune = false;
            Physics2D.IgnoreLayerCollision(projectileLayer, gameObject.layer, false);
        }
    }
    public IEnumerator FlashCoroutine(float duration, Color flashColor, int flashCount)
    {
        Color startColor = playerSprite.color;
        float elapsedFlashTime = 0;
        float elapsedFlashPercent = 0;
        while (elapsedFlashTime < duration)
        {
            elapsedFlashTime += Time.deltaTime;
            elapsedFlashPercent = elapsedFlashTime/duration;
            if (elapsedFlashPercent > 1f)
            {
                elapsedFlashPercent = 1f;
            }
            float pingPongPercent = Mathf.PingPong(elapsedFlashPercent * 2 * flashCount, 1);
            playerSprite.color = Color.Lerp(startColor, flashColor, pingPongPercent);
            yield return null;
        }
    }
    // private void OnTriggerEnter2D(Collider2D collision) {
    //     if (gameObject.layer == LayerMask.NameToLayer("Combat") && collision.gameObject.layer == LayerMask.NameToLayer("Damageable"))
    //     {
    //         Damage(10f);
    //     }
    // }
}
