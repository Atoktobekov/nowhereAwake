using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogiaAnimator : MonoBehaviour
{
    public GameObject buttonDiolog;
    
    public DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonDiolog.SetActive(true);
        }
    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (buttonDiolog != null)
        {
            buttonDiolog.SetActive(false);
            
        }

        dialogueManager.EndDialogia();
    }
}
