using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineObject : MonoBehaviour
{
    public bool playerDetected;
    private PlayerInputHandler inputHandler;
    [SerializeField]private ExamineWindowUI examineWindow;
    private bool isExamine;
    private Sprite itemImage;
    [Header("Examine")]
    [TextArea(2, 10)]
    public string descriptionText;
    void Awake()
    {
         GameObject menu = GameObject.Find("Menu");
        if (menu != null)
        {
            Transform examineWindowTransform = menu.transform.Find("ExamineWindow");
            if (examineWindowTransform != null)
            {
                examineWindow = examineWindowTransform.gameObject.GetComponent<ExamineWindowUI>();
                examineWindow.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Examine Window not found");
            }
        }
        inputHandler = FindObjectOfType<PlayerInputHandler>();
        itemImage = GetComponent<SpriteRenderer>().sprite;
        playerDetected = false;
        isExamine = false;
    }

    void Update()
    {
        if (playerDetected && !isExamine)
        {
            if (inputHandler.ExamineInput)
            {
                examineWindow.Show();
                examineWindow.objectImage.sprite = itemImage;
                examineWindow.objectTitle.text = gameObject.name;
                examineWindow.objectDescription.text = descriptionText;
                isExamine = true;
            }
        }
        else if (isExamine)
        {
            if (!inputHandler.ExamineInput)
            {
                examineWindow.Hide();
                examineWindow.ResetExamineWindow();
                isExamine = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerDetected = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerDetected = false;
        }    
    }
}
