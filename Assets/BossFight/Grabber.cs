using UnityEngine;
using System.Collections;

public class Grabber : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;
    [SerializeField] float speed = 3f;
    float distance = 1000;
    [SerializeField] float offset = 0f;

    bool isDestroying = false;
    Animator anim;

    void Start()
    {
        var go = GameObject.FindWithTag("Player");
        player = go.GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (player == null)
        {
            Debug.Log("Player not found");
            return;
        }

        // finding distance to player
        Vector2 playerPos = player.transform.position;
        Vector2 myPos = rb.position;

        distance = Vector2.Distance(playerPos, myPos);
        Vector2 direction = (playerPos - myPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        

        //attack when in distance
        if (distance > 0.5f && !isDestroying)
        {
            rb.linearVelocity = direction * speed;
            rb.rotation = angle + offset;
        }
        else // if (!isDestroying)
        {
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        anim.SetTrigger("Attack");
        isDestroying = true;

        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
