using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;
    private ParticleManager ParticleManager {get => particleManager ?? core.GetCoreComponent(ref particleManager);}
    private ParticleManager particleManager;
    private Stats Stats {get => stats ??= core.GetCoreComponent<Stats>();}
    private Stats stats;
    public Drops drops;
    public float dropProbability;
    private KillCounterBar killCounterBar;
    [SerializeField] private SO_AggressiveWeaponData playerSword;
    [SerializeField] private SO_RangedWeaponData playerBow;
    public void Die()
    {
        foreach (var particle in deathParticles)
        {
            ParticleManager.StartParticlesAtOrigin(particle);
        }
    
        if (gameObject.layer == LayerMask.NameToLayer("Player")) {
            core.transform.parent.gameObject.SetActive(false);
            core.transform.parent.transform.position = Vector3.zero;
            FindObjectOfType<GameOverManager>().GameOver();
            stats.currentHealth = stats.maxHealth;
            core.transform.parent.gameObject.SetActive(true);
        }
        else if (core.transform.parent.gameObject.name == "Boss1")
        {
            Debug.Log("Boss1 defeated");
            Instantiate(core.transform.parent.GetComponent<Boss1>().dropItem, core.transform.position, Quaternion.identity);
            FindObjectOfType<KillCounterBar>().Boss1Killed = true;
            playerSword.AttackDetails[0].damageAmount += 10;
            playerSword.AttackDetails[1].damageAmount += 10;
            playerSword.AttackDetails[2].damageAmount += 10;
            playerBow.projectileDamage += 5;
            core.transform.parent.GetComponent<Boss1>().endPoint.SetActive(true);
            core.transform.parent.GetComponent<Boss1>().endPointTile.SetActive(true);
            core.transform.parent.gameObject.SetActive(false);
        }
        else if (core.transform.parent.gameObject.name == "Boss2")
        {
            Debug.Log("Boss2 defeated");
            Instantiate(core.transform.parent.GetComponent<Boss2>().dropItem, core.transform.position, Quaternion.identity);
            FindObjectOfType<KillCounterBar>().Boss2Killed = true;
            playerSword.AttackDetails[0].damageAmount += 10;
            playerSword.AttackDetails[1].damageAmount += 10;
            playerSword.AttackDetails[2].damageAmount += 10;
            playerBow.projectileDamage += 5;
            core.transform.parent.GetComponent<Boss2>().endPoint.SetActive(true);
            core.transform.parent.GetComponent<Boss2>().endPointTile.SetActive(true);
            core.transform.parent.gameObject.SetActive(false);
        }
        else if (core.transform.parent.gameObject.name == "Boss3")
        {
            Debug.Log("Boss3 defeated");
            Instantiate(core.transform.parent.GetComponent<Boss1>().dropItem, core.transform.position, Quaternion.identity);
            FindObjectOfType<KillCounterBar>().Boss3Killed = true;
            playerSword.AttackDetails[0].damageAmount += 15;
            playerSword.AttackDetails[1].damageAmount += 15;
            playerSword.AttackDetails[2].damageAmount += 15;
            playerBow.projectileDamage += 10;
            core.transform.parent.GetComponent<Boss1>().endPoint.SetActive(true);
            core.transform.parent.GetComponent<Boss1>().endPointTile.SetActive(true);

            core.transform.parent.gameObject.SetActive(false);
        }
        else if (core.transform.parent.gameObject.name == "Boss4")
        {
            Debug.Log("Boss4 defeated");
            Instantiate(core.transform.parent.GetComponent<Boss4>().dropItem, core.transform.position, Quaternion.identity);
            FindObjectOfType<KillCounterBar>().Boss4Killed = true;
            playerSword.AttackDetails[0].damageAmount += 15;
            playerSword.AttackDetails[1].damageAmount += 15;
            playerSword.AttackDetails[2].damageAmount += 15;
            playerBow.projectileDamage += 10;
            core.transform.parent.GetComponent<Boss4>().endPoint.SetActive(true);
            core.transform.parent.GetComponent<Boss4>().endPointTile.SetActive(true);
            core.transform.parent.gameObject.SetActive(false);
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Damageable"))
        {
            Debug.Log(core.transform.parent.gameObject.name);
            Debug.Log(core.transform.parent.name);
            drops = core.dropList;
            dropProbability = Random.Range(0f, 1f);
            if (dropProbability > 0.5 && dropProbability <= 0.9)
            {
                Instantiate(drops.dropList[0], core.transform.position, Quaternion.identity);
            }
            else if (dropProbability <= 0.55 && dropProbability > 0.3)
            {
                Instantiate(drops.dropList[1], core.transform.position, Quaternion.identity);
            }
            else if (dropProbability <= 0.2 && dropProbability > 0.1)
            {
                Instantiate(drops.dropList[2], core.transform.position, Quaternion.identity);
            }
            killCounterBar = FindObjectOfType<KillCounterBar>();
            killCounterBar.currentKillCount ++;
            core.transform.parent.gameObject.SetActive(false);
        }
        

    }
    private void OnEnable()
    {
        Stats.OnHealthZero += Die;
    }
    private void OnDisable()
    {
        Stats.OnHealthZero -= Die;    
    }
}
