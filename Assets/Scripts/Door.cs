using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [ContextMenu(itemName:"open")]
    public void Open()
    {
        animator.SetTrigger(name: "open");
    }


}
