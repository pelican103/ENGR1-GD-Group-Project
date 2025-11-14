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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var go = GameObject.FindWithTag("Player");
        player = go.GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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
        rb.rotation = angle + offset;

        //attack when in distance
        if (distance > 0.5f)
        {
            rb.linearVelocity = direction * speed;
        }
        else if (!isDestroying)
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
