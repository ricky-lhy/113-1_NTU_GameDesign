using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image borderImage;
    public event Action <UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;
    private bool empty = true;

    public void Awake()
    {
        borderImage = transform.GetChild(0).GetComponent<Image>();
        itemImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        if (itemImage == null)
        {
            Debug.Log("Cannot get itemImage in ItemUI");
        }
        quantityText = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);
        empty = true;
    }
    public void Deselect()
    {
        borderImage.enabled = false;
    }
    public void SetData(Sprite sprite, int quantity)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        quantityText.text = quantity + "";
        empty = false;
    }
    public void Select()
    {
        borderImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Clicked");
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
            Debug.Log("Left Clicked");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

}
