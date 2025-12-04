using UnityEngine;

public class HealthRefill : MonoBehaviour
{
    [SerializeField] private float restoreAmount = 10f; 
    [SerializeField] private AudioClip pickupSound; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.IncreaseHealth(restoreAmount);

            if (pickupSound)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject); 
        }
    }
}
