using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour, IInteractable
{
    private Animator animator;
    public bool isOpened { get; private set; } = false;

    [SerializeField] private UnityEvent onOpen;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [ContextMenu(itemName:"opened")]
    public void Open()
    {
        animator.SetTrigger(name: "opened");
        onOpen.Invoke();
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

    public void Next()
    {
        return;
    }

}