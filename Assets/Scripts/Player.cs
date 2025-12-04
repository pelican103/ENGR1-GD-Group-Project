using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class Player : MonoBehaviour
{
    [SerializeField]    float   speed           = 6 ;
    [SerializeField]    int     rollCD          = 50;
    [SerializeField]    int     rollDistance    = 6 ;
    [SerializeField]    int     rollSpeed       = 12;
    [SerializeField]    int     rollIframes     = 6 ;
    [SerializeField]    int     attackCD        = 6 ;
    [SerializeField]    float   attackDMG       = 6 ;
    [SerializeField]    int     attackLinger    = 6 ;

    public             int     action_counter  = 0 ;
    private             int     attack_counter  = 0 ;
    public             int     roll_counter    = 0 ;

    private             Vector2         input2d         ;
    private             Vector2         lockedinput     ;
    private             Vector2         direction       ;

    private             Rigidbody2D     rb              ;
    private             Animator        animator        ;
    private             SpriteRenderer  spriteRenderer  ;
    private             GameObject      pen             ;
    public              GameObject      SkillPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize Variables
        pen             = this.gameObject.transform.GetChild(0).gameObject  ;
        rb              = GetComponent<Rigidbody2D>     ()                  ;
        animator        = GetComponent<Animator>        ()                  ;
        spriteRenderer  = GetComponent<SpriteRenderer>  ()                  ;
        direction       = Vector2.down                                      ;
        SkillPanel      = SkillManager.Instance.gameObject.GetComponent<Transform>().GetChild(0).gameObject ;
        pen.SetActive ( false ) ;
    }

    void Update()
    {
        if (animator==null) return;
        
        animator.SetInteger ("Dir"      , (int)(direction.y + (2 * direction.x)));
        animator.SetBool    ("Moving"   , input2d != Vector2.zero);
    }

    private void FixedUpdate()
    {
        float tSpeed = speed;
        Vector2 dir = input2d;
        if (action_counter != 0) action_counter--;
        if (attack_counter != 0)
        {
            pen.SetActive (attack_counter != 1);
            attack_counter--;
        }

        if (roll_counter != 0)
        {
            tSpeed = rollSpeed;
            dir = lockedinput;
            roll_counter--;
        }

        // Physics Handling

        if (rb == null) return;

        rb.linearVelocity = dir * tSpeed * Time.deltaTime;
        pen.transform.localPosition = direction;
    }

    void OnMove(InputValue value)
    {
        //Ensure Vector is Normalized so speed isn't increase
        //when moving diagonally
        input2d = value.Get<Vector2>().normalized;
        if(input2d != Vector2.zero && action_counter == 0 && Vector2.Dot(input2d, direction) <= 0)
        {
            direction = input2d;
        }
    }

    void OnAttack(InputValue value)
    {
        if (action_counter != 0) return;
        action_counter = attackCD;
        attack_counter = attackLinger;
        pen.SetActive(true);
    }

    void OnMenu(InputValue value)
    {
        bool newState = !SkillPanel.activeSelf;
        SkillPanel.SetActive(newState);
    }

    void OnJump(InputValue value)
    {
        Debug.Log("Roll!");
        if (action_counter != 0) return;
        action_counter = rollCD;
        roll_counter = rollDistance;
        lockedinput = (input2d == Vector2.zero) ? direction : input2d;
    }
}
