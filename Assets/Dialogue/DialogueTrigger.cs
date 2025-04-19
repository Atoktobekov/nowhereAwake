using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;
    public GameObject DialogueButton;
    public Image NPCImage;

    public void TriggerDialogue()
    {
        DialogueButton.SetActive(false);
        dialogueManager.StartDialogue(dialogue);
        Debug.Log("StartDialogue");
    }
}
