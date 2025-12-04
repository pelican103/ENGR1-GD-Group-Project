using UnityEngine;

public class EventOnPickup : MonoBehaviour
{
    [SerializeField] GameObject otherObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            otherObject.SetActive(false);
        }
    }
}
