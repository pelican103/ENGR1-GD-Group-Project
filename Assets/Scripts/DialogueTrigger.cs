using System.Collections.Generic;
using UnityEngine;
 
[System.Serializable]
public class Dialogue
{
    public List<string> dialogueLines = new List<string>();
}

 
public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    public bool isTriggered { get; private set; } = false;
 
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void OnNext()
    {
        DialogueManager.Instance.SpeedUp();
    }
 
    public bool IsInteractable()
    {
        return !isTriggered;
    }

    public void Interact()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            TriggerDialogue();
        }
    }
}
 

