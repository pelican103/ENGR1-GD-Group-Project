using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField]    float           speed = 6       ;
    private             Rigidbody2D     rb              ;
    private             Animator        animator        ;
    private             SpriteRenderer  spriteRenderer  ;
    private             Vector2         input2d         ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize Variables
        rb              = GetComponent<Rigidbody2D>     ();
        animator        = GetComponent<Animator>        ();
        spriteRenderer  = GetComponent<SpriteRenderer>  ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Physics Handling
        rb.linearVelocity = input2d * speed * Time.deltaTime;
    }

    void OnMove(InputValue value)
    {
        //Ensure Vector is Normalized so speed isn't increase
        //when moving diagonally
        input2d = value.Get<Vector2>().normalized; 
    }
}
