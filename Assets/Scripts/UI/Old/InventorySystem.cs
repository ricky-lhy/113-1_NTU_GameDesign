using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public GameObject obj;
        public int stack = 1;
        public InventoryItem(GameObject o, int s=1)
        {
            obj = o;
            stack = s;
        }
    }
    [Header("General Fields")]
    // List of items picked up
    public List<InventoryItem> items = new List<InventoryItem>();
    public bool isOpen;
    [Header("UI Items")]
    public GameObject ui_Window;
    public Image[] items_images;
    [Header("UI Item Description")]
    public GameObject ui_Description_Window;
    public Image description_Image;
    public Text description_Title;
    public Text description_Text;
    public Text coin_Value;
    private float current_Coin;

    private void Start() {
        current_Coin = 0;
        coin_Value.text = current_Coin.ToString();
    }
    private void Update()
    {
        coin_Value.text = current_Coin.ToString();
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleInventory();
        }    
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        ui_Window.SetActive(isOpen);
        if (ui_Window.activeSelf)
        {
            Time.timeScale = 0f;
            Update_UI();
        }
        else
        {
            Time.timeScale = 1f;
            Update_UI();
        }
    }

    //Add item to the item list
    public void PickUp(GameObject item)
    {
        if (item.GetComponent<OldItem>().stackable)
        {
            //Check if there is same item existing in inventory
            InventoryItem existingItem = items.Find(x=>x.obj.name==item.name);
            if (existingItem != null)
            {
                existingItem.stack += item.GetComponent<OldItem>().stack;
                Debug.Log("Pick up " + item.name + " x" + item.GetComponent<OldItem>().stack);
            }
            else
            {
                InventoryItem i = new InventoryItem(item);
                items.Add(i);
                Debug.Log("Pick up " + item.name + " x1");
            }
        }
        else
        {
            InventoryItem i = new InventoryItem(item);
            items.Add(i);
            Debug.Log("Pick up " + item.name);
        }    
        Update_UI();
    }
    public void PickUpCoin(GameObject item)
    {
        int pickedCoin = Random.Range(1, 35);
        current_Coin += pickedCoin;
        // current_Coin += item.GetComponent<Item>().stack;
        coin_Value.text = current_Coin.ToString();
        Debug.Log("Pick up coins x" + pickedCoin);
    }

    public bool CanPickUp()
    {
        if (items.Count >= items_images.Length)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //Refresh UI elements in inventory window
    void Update_UI()
    {
        HideAll();
        //For each item in list, show its image in the slot
        for(int i = 0; i < items.Count; i++)
        {
            items_images[i].sprite = items[i].obj.GetComponent<SpriteRenderer>().sprite;
            items_images[i].gameObject.SetActive(true);
        }
        
    }
    void HideAll()
    {
        HideDescription();
        foreach(var i in items_images) 
            i.gameObject.SetActive(false);
    }
    public void ShowDescription(int id)
    {   
        //Set image
        description_Image.sprite = items_images[id].sprite;
        //Set description
        if (items[id].stack == 1) //If stack == 1, only show the name
            description_Title.text = items[id].obj.name;
        else //If stack > 1, show name + stack
            description_Title.text = items[id].obj.name + " x" + items[id].stack;
        description_Text.text = items[id].obj.GetComponent<OldItem>().descriptionText;
        //Show the elements
        description_Image.gameObject.SetActive(true);
        description_Title.gameObject.SetActive(true);
        description_Text.gameObject.SetActive(true);
    }
    public void HideDescription()
    {
        description_Image.gameObject.SetActive(false);
        description_Title.gameObject.SetActive(false);
        description_Text.gameObject.SetActive(false);
    }

    public void Consume(int id)
    {
        if (items[id].obj.GetComponent<OldItem>().type == OldItem.ItemType.Consumables)
        {
            Debug.Log($"CONSUMED {items[id].obj.name}");
            //Invoke consume event
            items[id].obj.GetComponent<OldItem>().consumeEvent.Invoke();
            //Reduce the stack number
            items[id].stack --;
            if (items[id].stack == 0)
            {
                //Delete item from list
                Destroy(items[id].obj, 0.1f);
                items.RemoveAt(id);
            }
            Update_UI();
        }
    }
}
