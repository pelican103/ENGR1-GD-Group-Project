using UnityEngine;

public class VisionRestorePickup : MonoBehaviour
{
    [SerializeField] private float restoreAmount = 1.5f; 
    [SerializeField] private AudioClip pickupSound; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        VisionShrink vision = other.GetComponent<VisionShrink>();
        if (vision != null)
        {
            vision.RestoreVision(restoreAmount);

            if (pickupSound)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject); 
        }
    }
}
