using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private Animator animator;
    public bool isOpened { get; private set; }
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [ContextMenu(itemName:"opened")]
    public void Open()
    {
        animator.SetTrigger(name: "opened");
    }

    public bool IsInteractable()
    {
        return !isOpened;
    }

    public void Interact()
    {
        if (!isOpened)
        {
            isOpened = true;
            Open();
        }
    }

}