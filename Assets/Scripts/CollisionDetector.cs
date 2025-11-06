using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField]
    private string colliderScript;

    [SerializeField]
    private UnityEvent collisionEnter;

    [SerializeField]
    private UnityEvent collisionExit;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent(colliderScript))
        {
            collisionEnter?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.GetComponent(colliderScript))
        {
            collisionExit?.Invoke();
        }
    }


}
