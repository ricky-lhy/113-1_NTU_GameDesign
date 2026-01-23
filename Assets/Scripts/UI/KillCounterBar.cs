using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class KillCounterBar : MonoBehaviour
{
    [SerializeField] private LevelTargetData level1_Data;
    [SerializeField] private LevelTargetData level2_Data;
    [SerializeField] private LevelTargetData level3_Data;
    [SerializeField] private LevelTargetData level4_Data;
    [SerializeField] public GameObject player;
    [SerializeField] public TMP_Text killCountText;
    public int targetKillCount;
    public int currentKillCount;
    public bool Boss1Killed;
    public bool Boss2Killed;
    public bool Boss3Killed;
    public bool Boss4Killed;
    void Start()
    {
        currentKillCount = 0;
        Boss1Killed = false;
        Boss2Killed = false;
        Boss3Killed = false;
        Boss4Killed = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        targetKillCount = GetTargetKillCount();
        if (targetKillCount == -1)
        {
            killCountText.text = "已擊殺數: N/A";
            currentKillCount = 0;
        }
        else
        {
            killCountText.text = "已擊殺數: " + currentKillCount + "/" + targetKillCount;
        }
        switch (GetLastBossKilled())
        {
            case 4: player.GetComponentInChildren<Stats>().maxHealth = 350;
                break;
            case 3: player.GetComponentInChildren<Stats>().maxHealth = 300;
                break;
            case 2: player.GetComponentInChildren<Stats>().maxHealth = 250;
                break;
            case 1: player.GetComponentInChildren<Stats>().maxHealth = 200;
                break;
            default: player.GetComponentInChildren<Stats>().maxHealth = 150;
                break;
        };
    }
    int GetTargetKillCount()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name switch
        {
            "Level A1" => level1_Data.enemyNeedToKill,
            "Level A2" => level2_Data.enemyNeedToKill,
            "Level A3" => level3_Data.enemyNeedToKill,
            "Level A4" => level4_Data.enemyNeedToKill,
            _ => -1,
        };
    }
    public void AddKillCount()
    {
        currentKillCount++;
    }
    public int GetLastBossKilled()
    {
        if (Boss4Killed) return 4;
        if (Boss3Killed) return 3;
        if (Boss2Killed) return 2;
        if (Boss1Killed) return 1;
        return 0;
    }
    public bool CheckKillCountMatched()
    {
        return currentKillCount >= targetKillCount;
    }
}
