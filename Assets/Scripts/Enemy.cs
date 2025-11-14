using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float chaseSpeed = 3f;
    [SerializeField] float stopDistance = 0.8f;

    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackCooldown = 1.5f;
    [SerializeField] GameObject AttackHitbox;

    [SerializeField] Transform player;
    [SerializeField] Rigidbody2D enemy;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform currentTarget;
    [SerializeField] Animator animator;

    
    private bool playerInRange = false;
    private float lastAttackTime = -99f;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        currentTarget = pointA;
        
        if (AttackHitbox != null)
            AttackHitbox.SetActive(false);
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

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        animator.SetFloat("Speed", enemy.linearVelocity.magnitude);

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
 
        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
        // Only chase if outside the stop distance
        else if (distance > stopDistance)
        {
            enemy.linearVelocity = direction * chaseSpeed;
            animator.SetFloat("Speed", enemy.linearVelocity.magnitude);
        }
        // If inside stop distance but not attacking, stop
        else
        {
            enemy.linearVelocity = Vector2.zero;
            animator.SetFloat("Speed", 0f);
        }

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

    }

    void Attack()
    {
        lastAttackTime = Time.time;
        
        enemy.linearVelocity = Vector2.zero;
        animator.SetFloat("Speed", 0f);

        animator.SetTrigger("Attack"); 
        Debug.Log("Attacking!");
    }


    public void EnableAttackHitbox()
    {
        Debug.Log("WOWWWWWW");
        if (AttackHitbox != null)
        {
            Debug.Log("Hitbox Enabled");

            AttackHitbox.SetActive(true);
        }
    }

    public void DisableAttackHitbox()
    {
        if (AttackHitbox != null)
        {
            Debug.Log("Hitbox Disabled");
            AttackHitbox.SetActive(false);
            
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
/*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
*/
}
