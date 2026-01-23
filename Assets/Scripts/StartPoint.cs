using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> hiddenObjectsAfterClear;
    [SerializeField] private  InventorySO playerInventoryData;
    [SerializeField] private List<ItemSO> targetKeys;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private GameObject endPointTile;
    private KillCounterBar killCounterBar;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        Scene currentScene = SceneManager.GetActiveScene();
        killCounterBar = FindObjectOfType<KillCounterBar>();
        if (currentScene.name == "Lobby")
        {
            GameObject.Find("Player").GetComponentInChildren<Stats>().currentHealth = GameObject.Find("Player").GetComponentInChildren<Stats>().maxHealth;
            GameObject.Find("Player").GetComponentInChildren<Stats>().currentMana = GameObject.Find("Player").GetComponentInChildren<Stats>().maxMana;
        }
        if (CheckClear())
        {
            hiddenObjectsAfterClear.ForEach(obj => obj.SetActive(false));
        }
        else if (endPoint && endPointTile)
        {
            endPoint.SetActive(false);
            endPointTile.SetActive(false);
        }
        if (playerInventoryData != null)
        {
            for (int index = 0; index < playerInventoryData.inventoryItems.Count; index++)
            {
                InventoryItem item = playerInventoryData.inventoryItems[index];
                if (targetKeys.Contains(item.item))
                {
                    playerInventoryData.RemoveItem(index, 1);
                }
            }
        }
        if (player != null)
        {
            player.transform.position = transform.position;
        }
    }

    public bool CheckClear()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name switch
        {
            "Level A1" => killCounterBar.GetLastBossKilled() >= 1,
            "Level A2" => killCounterBar.GetLastBossKilled() >= 2,
            "Level A3" => killCounterBar.GetLastBossKilled() >= 3,
            "Level A4" => killCounterBar.GetLastBossKilled() >= 4,
            _ => false,
        };
    }
}
