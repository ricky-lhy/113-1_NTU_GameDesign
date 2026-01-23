using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider2D))]
public class OldItem : MonoBehaviour
{
    //Interaction Type
    public enum InteractionType {NONE, PickUp, Examine, Coin, Treasure}
    public enum ItemType {Static, Consumables}
    [Header("Attribute")]
    public InteractionType interactType;
    public ItemType type;
    public int stack;
    public bool stackable = false;
    [Header("Examine")]
    [TextArea(2, 10)]
    public string descriptionText;
    [Header("Event")]
    public UnityEvent customEvent;
    public UnityEvent consumeEvent;
    public Collider2D itemCollider;

    public Rigidbody2D rb;
    void Awake()
    {
        itemCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Reset() 
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 11;
    }

    public void Interact()
    {
        switch(interactType)
        {
            case InteractionType.PickUp:
                if (!FindObjectOfType<InventorySystem>().CanPickUp())
                    return;
                //Add to item list
                FindObjectOfType<InventorySystem>().PickUp(gameObject);
                gameObject.SetActive(false);
                break;
            case InteractionType.Examine:
                //Call examine item in interaction system
                FindObjectOfType<InteractionSystem>().ExamineItems(this);
                Debug.Log("Examine");
                break;
            case InteractionType.Coin:
                FindObjectOfType<InventorySystem>().PickUpCoin(gameObject);
                Destroy(gameObject);
                break;
            case InteractionType.Treasure:
                GetComponent<TreasureBox>().OpenChest();
                break;
            default:
                break;
        }
        customEvent.Invoke();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            itemCollider.isTrigger = true;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            // Debug.Log("Item "+gameObject.name+" Hit Ground");
        }
    }
}
