using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI dialogueText;
    public bool isDialogueActive = false;
     
    public float textSpeed;

    private List<string> dialogueLines = new List<string>();
    private bool isTyping = false;
    private int index = 0;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SpeedUp()
    {
        Debug.Log("Next pressed");
        if(dialogueText.text == dialogueLines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[index];
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void StartDialogue(Dialogue text)
    {
        isDialogueActive = true;
        animator.Play("ShowDialogue");
        dialogueText.text = "";

        for (int i = 0; i < text.dialogueLines.Count; i++)
        {
            dialogueLines.Add(text.dialogueLines[i]);
        }
        
        index = 0;
        StartCoroutine(TypeLine());

    }

    void NextLine()
    {
        if (index < dialogueLines.Count - 1)
        {
            index++;
            dialogueText.text = "";
            StopAllCoroutines();
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char letter in dialogueLines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("HideDialogue");
    }

    
}
