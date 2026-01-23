using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LockedWall : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    private DialogueManager dialogueManager;
    public Dialogue message;
    public InventorySO playerInventoryData;
    public ItemSO targetKey;
    public int index;
    public int targetKeyIndex;
    public bool find;
    private AudioSource audioSource;
    public AudioClip unlockSound;
    private KillCounterBar killCounterBar;
    void Start()
    {
        find = false;
        inputHandler = FindObjectOfType<PlayerInputHandler>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        audioSource = GetComponent<AudioSource>();
        message = new Dialogue();
        message.sentences = new string[1];
        killCounterBar = FindObjectOfType<KillCounterBar>();
    }

    // Update is called once per frame
    void Update()
    {
        index = 0;
        foreach (InventoryItem item in playerInventoryData.inventoryItems)
        {
            if (item.item == targetKey)
            {
                targetKeyIndex = index;
                find = true;
                break;
            }
            else
                find = false;
            index++;
        }
    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (find)
            {
                if (gameObject.tag == "BossGate" && !killCounterBar.CheckKillCountMatched())
                {
                    message.name = "[系統]";
                    message.sentences[0] = "通往更深處的道路需要淨化更多夢魘......";
                    PopUpMessage();
                    return;
                }
                audioSource.PlayOneShot(unlockSound);
                message.name = "[系統]";
                message.sentences[0] = "通路已開啟。";
                PopUpMessage();
                playerInventoryData.RemoveItem(targetKeyIndex, 1);
                gameObject.SetActive(false);
            }
            else
            {
                message.name = "[系統]";
                message.sentences[0] = "此處已鎖上，你並未持有開啟此處的物品。";
                PopUpMessage();
            }
        }

    }
    private void PopUpMessage()
    {
        dialogueManager.StartDialogue(message, DialogueManager.Mode.Notification);
    }
}
