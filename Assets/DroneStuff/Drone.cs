using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour
{
    float direction = 0;
    [SerializeField] float visionDist = 5f;
    float recover = 1.5f;
    bool located = false;

    Animator anim;

    [SerializeField] GameObject porj; 
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Direction", direction);
        //Debug.Log(direction);
    }

    void FixedUpdate()
    {
        //continulally rotates (for animation)
        if (!located)
        {
            if (direction < 4f)
            {
                direction += .02f;
            }
            else
            {
                direction = 0;
            }
        }
        else if (recover > 0)
        {
            recover -= .02f;
        }
        else
        {
            recover = 1.5f;
            direction += 1f;
            located = false;
        }

        if (!located)
        {
                    DetectPlayer();
        }
    }

    void DetectPlayer()
    {
        Vector2 dir = Vector2.zero;

        if (direction < 1f)
        {
            dir = Vector2.down;
        }
        else if (direction < 2f)
        {
            dir = Vector2.left;
        }
        else if (direction < 3f)
        {
            dir = Vector2.up;
        }
        else
        {
            dir = Vector2.right;
        }

        //raycast forward
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, visionDist);

        // to visualize
        //Debug.DrawRay(transform.position, dir * visionDist, Color.red);

        //check for player hit
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            located = true;
            if (direction < 1f)
            {
                anim.SetTrigger("AtkDown");
            }
            else if (direction < 2f)
            {
                anim.SetTrigger("AtkLeft");
            }
            else if (direction < 3f)
            {
                anim.SetTrigger("AtkUp");
            }
            else
            {
                anim.SetTrigger("AtkRight");
            }
            StartCoroutine(ShootProjectile());
        }
    }

    IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(0.2f);
        
        GameObject projo = Instantiate(porj, transform.position, Quaternion.identity);
        Projectile projectileScript = projo.GetComponent<Projectile>();
        projectileScript.Init(this);
        //Debug.Log("spawned");
    }

    public float GetDirection()
    {
        return (direction);
    }
    
    public float GetDistance()
    {
        return (visionDist);
    }
}
