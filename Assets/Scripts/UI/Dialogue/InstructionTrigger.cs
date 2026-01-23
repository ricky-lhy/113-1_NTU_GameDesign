using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, DialogueManager.Mode.Instruction);
        }
    }
}
