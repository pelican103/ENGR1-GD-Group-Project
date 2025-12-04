using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;

    public void OnInteract(InputValue value)
    {
        interactableInRange?.Interact();
    }

    public void OnNext(InputValue value)
    {
        Debug.Log("Next pressed in InteractionDetector");
        interactableInRange?.Next();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.IsInteractable())
        {
            Debug.Log("Interactable in range");
            interactableInRange = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
        }
    }

}
