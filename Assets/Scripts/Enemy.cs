using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float stopDistance = 0.8f;

    [SerializeField] Transform player;
    [SerializeField] Rigidbody2D enemy;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform currentTarget;

    private bool playerInRange = false;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentTarget = pointA;

    }

    void Update()
    {
        if (playerInRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

    }

    void Patrol()
    {
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        enemy.linearVelocity = direction * speed;

        spriteRenderer.flipX = direction.x < 0;

        float checkpoint_distance = Vector2.Distance(transform.position, currentTarget.position);
        if (checkpoint_distance < 0.2f)
        {
            if (currentTarget == pointA)
            {
                currentTarget = pointB;
            }
            else
            {
                currentTarget = pointA;
            }
        }
    }
    void ChasePlayer()
    {
        //calculate direction towards player
        Vector2 direction = (player.position - transform.position).normalized;

        float distance = Vector2.Distance(transform.position, player.position);

        //flippy!
        if (player.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        //stop when close enough
        if (distance > stopDistance)
        {
            enemy.linearVelocity = direction * speed;
        }
        else
        {
            enemy.linearVelocity = Vector2.zero;

        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
