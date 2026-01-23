using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ExamineWindowUI : MonoBehaviour
{
    public Image objectImage;
    public TMP_Text objectTitle;
    public TMP_Text objectDescription;
    void Awake()
    {
        objectImage = transform.GetChild(0).GetComponent<Image>();
        objectTitle = transform.GetChild(1).GetComponent<TMP_Text>();
        objectDescription = transform.GetChild(2).GetComponent<TMP_Text>();
        
    }
    void Update()
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
        InputSystem.DisableDevice(Mouse.current);
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        InputSystem.EnableDevice(Mouse.current);
    }
    public void ResetExamineWindow()
    {
        objectTitle.text = "";
        objectDescription.text = "";
    }
}
