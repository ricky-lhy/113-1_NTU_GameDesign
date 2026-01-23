using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] public InventoryDescription itemDescription;
    [SerializeField] private MouseFollower mouseFollower;
    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();
    private int currentDraggedItemIndex = -1;
    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;
    [SerializeField] private ItemActionPanel actionPanel;
    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }
    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            listOfUIItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }
    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    private void HandleShowItemActions(UIInventoryItem item)
    {
        int index = listOfUIItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnItemActionRequested?.Invoke(index);
    }

    private void HandleEndDrag(UIInventoryItem item)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(UIInventoryItem item)
    {
        int index = listOfUIItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentDraggedItemIndex, index);
        HandleItemSelection(item);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(UIInventoryItem item)
    {
        int index = listOfUIItems.IndexOf(item);
        if (index == -1)
            return;
        currentDraggedItemIndex = index;
        HandleItemSelection(item);
        OnStartDragging?.Invoke(index);
    }
    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }
    private void HandleItemSelection(UIInventoryItem item)
    {
        int index = listOfUIItems.IndexOf(item);
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }
    public void AddAction(string actionName, Action performAction)
    {
        actionPanel.AddButton(actionName, performAction);
    }
    public void ShowItemAction(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
    }
    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
        actionPanel.Toggle(false);
    }

    public void Hide()
    {
        actionPanel.Toggle(false);
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfUIItems[itemIndex].Select();
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }
}
