using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject indicator;

    private PlayerInputHandler inputHandler;
    private bool started = false;
    private bool playerDetected;
    public UnityEvent customEvent;
    private void Awake()
    {
        inputHandler = FindObjectOfType<PlayerInputHandler>();
    }

    private void Update()
    {
        if (!started && playerDetected && inputHandler.InteractInput)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, DialogueManager.Mode.Dialogue);
            started = true;
            indicator?.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerDetected = true;
            indicator?.SetActive(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerDetected = false;
            started = false;
            indicator?.SetActive(playerDetected);
        }
    }
}
