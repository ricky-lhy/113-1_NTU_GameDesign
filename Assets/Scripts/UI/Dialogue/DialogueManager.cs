using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public int letterPerSecond = 120;

    public enum Mode { Dialogue, Notification, Instruction };

    private bool started = false;
    private bool waiting = false;
    private Mode mode = Mode.Dialogue;
    private Queue<string> sentences;
    private PlayerInputHandler inputHandler;

    private Transform dialogueObject;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;
    private TextMeshProUGUI dialogueHint;

    private KillCounterBar killCounterBar;

    void Awake()
    {
        inputHandler = GameObject.Find("Player").GetComponent<PlayerInputHandler>();
        Transform canvas = GameObject.Find("Canvas").transform;
        dialogueObject = canvas.Find("Dialogue");
        nameText = dialogueObject.Find("DialogueSpeaker").GetComponentInChildren<TextMeshProUGUI>();
        dialogueText = dialogueObject.Find("DialogueSentence").GetComponentInChildren<TextMeshProUGUI>();
        dialogueHint = dialogueObject.Find("DialogueHint").GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        sentences = new Queue<string>();
        killCounterBar = FindObjectOfType<KillCounterBar>();
    }

    void Update()
    {
        if (started && waiting && inputHandler.InteractInput)
        {
            waiting = false;
            dialogueHint.text = "";
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue, Mode dialogueMode)
    {
        sentences.Clear();
        dialogueHint.text = "";
        dialogueObject.gameObject.SetActive(true);
        mode = dialogueMode;
        started = true;
        switch (mode)
        {
            case Mode.Dialogue:
                Time.timeScale = 0;
                nameText.text = dialogue.name + ":";
                break;
            case Mode.Notification:
                nameText.text = "[系統訊息]";
                break;
            case Mode.Instruction:
                nameText.text = "操作指示 (" + dialogue.name + ")";
                break;
            default:
                break;
        }

        int lastBossKilled = killCounterBar.GetLastBossKilled();
        foreach (string sentence in dialogue.sentences)
        {
            if ((sentence.StartsWith("[0] ") && lastBossKilled == 0) ||
                (sentence.StartsWith("[1] ") && lastBossKilled == 1) ||
                (sentence.StartsWith("[2] ") && lastBossKilled == 2) ||
                (sentence.StartsWith("[3] ") && lastBossKilled == 3) ||
                (sentence.StartsWith("[4] ") && lastBossKilled == 4) ||
                (sentence.StartsWith("[A] ") && 0 < lastBossKilled && lastBossKilled < 4))
                sentences.Enqueue(sentence[4..]);
            if (!sentence.StartsWith("[0]") &&
                !sentence.StartsWith("[1]") &&
                !sentence.StartsWith("[2]") &&
                !sentence.StartsWith("[3]") &&
                !sentence.StartsWith("[4]") &&
                !sentence.StartsWith("[A]"))
                sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        switch (mode)
        {
            case Mode.Dialogue:
                StartCoroutine(TypeSentence(sentence));
                break;
            case Mode.Notification:
                dialogueText.text = sentence;
                Invoke("EndDialogue", 1.5f);
                break;
            case Mode.Instruction:
                dialogueText.text = sentence;
                dialogueHint.text = "按 [E] 關閉説明";
                waiting = true;
                break;
            default:
                break;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(1f / letterPerSecond);
        }
        waiting = true;
        dialogueHint.text = "[E]";
    }

    public void EndDialogue()
    {
        dialogueObject.gameObject.SetActive(false);
        Time.timeScale = 1;
        waiting = false;
        started = false;
    }

}