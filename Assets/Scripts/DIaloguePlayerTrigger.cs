using System.Collections.Generic;
using UnityEngine;

public class DialoguePlayerTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void Start()
    {
        TriggerDialogue();
    }
 
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void OnNext()
    {
        DialogueManager.Instance.SpeedUp();
    }
}