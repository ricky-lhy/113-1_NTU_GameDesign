using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;
    private DialogueManager dialogueManager;
    public Dialogue message;
    void Start(){
        dialogueManager = FindObjectOfType<DialogueManager>();
        message = new Dialogue();
        message.sentences = new string[1];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null && !item.isBeingTouched)
        {
            item.isBeingTouched = true;
            int remainder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            message.name = "[系統]";
            message.sentences[0] = "已獲得 " + item.InventoryItem.Name + " x" + item.Quantity;
            DialoguePopUp();
            if (remainder == 0)
                item.DestroyItem();
            else
                item.Quantity = remainder;
        }
    }
    private void DialoguePopUp()
    {
        dialogueManager.StartDialogue(message, DialogueManager.Mode.Notification);
    }
}
