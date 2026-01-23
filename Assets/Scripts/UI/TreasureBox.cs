using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(AudioSource))]

public class TreasureBox : MonoBehaviour 
{
    private PlayerInputHandler inputHandler;
    private GameObject boxItem;
    private Animator anim;
    public bool playerDetected;
    public bool isOpen;
    private AudioSource audioSource;
    public AudioClip OpenSound;
    private void Start() 
    {
        inputHandler = FindObjectOfType<PlayerInputHandler>();
        boxItem = transform.GetChild(0).gameObject;
        boxItem.SetActive(false);
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerDetected = false;
        isOpen = false;
    }   
    private void Update() 
    {
        if (playerDetected && inputHandler.InteractInput && !isOpen)
        {
            OpenChest();
        }
    }
    public void OpenChest()
    {
        isOpen = true;
        anim.SetBool("Open", isOpen);
        Debug.Log("Opening Chest");
    }
    public void LoadItem()
    {
        boxItem.SetActive(true);
        Debug.Log("The chest contains " + boxItem.name);
        // Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerDetected = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerDetected = false;
    }
    public void PlaySound()
    {
        audioSource.clip = OpenSound;
        audioSource.loop = false;
        audioSource.Play();
    }
}