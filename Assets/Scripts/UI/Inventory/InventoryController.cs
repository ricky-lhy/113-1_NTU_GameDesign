using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;
    [SerializeField] public InventorySO inventoryData;
    public PlayerInputHandler inputHandler;
    public GameObject playerStatsGameObject;
    // public List<InventoryItem> initialItems = new List<InventoryItem>();
    void Awake()
    {
        GameObject menu = GameObject.Find("Menu");
        if (menu != null)
        {
            Transform inventoryUITransform = menu.transform.Find("Inventory");
            if (inventoryUITransform != null)
            {
                inventoryUI = inventoryUITransform.gameObject.GetComponent<UIInventoryPage>();
            }
            else
            {
                Debug.Log("Inventory Page not found");
            }
        }
    }
    void Start()
    {
        PrepareUI();
        PrepareInventoryData();
        inputHandler = FindObjectOfType<PlayerInputHandler>();
        Transform core = transform.Find("Core");
        playerStatsGameObject = core.Find("Stats").gameObject;
    }

    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        // foreach (InventoryItem item in initialItems) 
        // {
        //     if (item.IsEmpty)
        //         continue;
        //     inventoryData.AddItem(item);

        // }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            inventoryUI.ShowItemAction(itemIndex);
            inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
        }
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryUI.AddAction("丟棄", ()=>DropItem(itemIndex, inventoryItem.quantity));
            // inventoryData.RemoveItem(itemIndex, 1);
        }
    }

    private void DropItem(int itemIndex, int quantity)
    {
        inventoryData.RemoveItem(itemIndex, quantity);
        inventoryUI.ResetSelection();
    }

    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(playerStatsGameObject);
            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
            {
                inventoryUI.ResetSelection();
            }
        }
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, 1);
        }
    }
    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }

    private void HandleSwapItems(int itemIndex1, int itemIndex2)
    {
        inventoryData.SwapItems(itemIndex1, itemIndex2);
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.item;
        inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Description);
    }

    void Update()
    {
        if (inputHandler.InventoryInput && !inventoryUI.isActiveAndEnabled)
        {
            Time.timeScale = 0;
            inventoryUI.Show();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }
        else if (!inputHandler.InventoryInput && inventoryUI.isActiveAndEnabled){
            inventoryUI.Hide();
            Time.timeScale = 1;
        }
    }
}
