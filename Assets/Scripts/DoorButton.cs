using UnityEngine;

public class DoorButton : MonoBehaviour
{
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [ContextMenu(itemName:"pressed")]
    public void Press()
    {
        animator.SetTrigger(name: "pressed");
    }
}
