using UnityEngine;

public class Projectile : MonoBehaviour
{

    Drone drone;
    float direction;
    Animator anim;
    [SerializeField] float moveSpeed = 5;

    public void Init(Drone parent)
    {
        drone = parent;
    }
    
    void Start()
    {
        anim = GetComponent<Animator>();


        //destoryt after 1 seconds
        Destroy(gameObject, 1);


        if (drone == null)
        {
            //Debug.LogWarning("Projectile missing Drone reference!");
            return;
        }
        
        direction = drone.GetDirection();

        if (direction < 1f)
        {
            anim.SetTrigger("Down");
        }
        else if (direction < 2f)
        {
            anim.SetTrigger("Left");
        }
        else if (direction < 3f)
        {
            anim.SetTrigger("Up");
        }
        else
        {
            anim.SetTrigger("Right");
        }
    }

    void Update()
    {
        Vector3 moveDir = Vector3.zero;

        if (direction < 1f) moveDir = Vector3.down;
        else if (direction < 2f) moveDir = Vector3.left;
        else if (direction < 3f) moveDir = Vector3.up;
        else moveDir = Vector3.right;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
