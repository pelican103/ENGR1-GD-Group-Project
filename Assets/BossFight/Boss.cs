using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{

    [SerializeField] GameObject grab;
    [SerializeField] GameObject wireList;

    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // check if all wires are gone
        // if so, play animation
        // 
    }
    
    IEnumerator SpawnGrabber()
    {
        yield return new WaitForSeconds(0.2f);
        
        GameObject newgrabby = Instantiate(grab, transform.position, Quaternion.identity);
        Grabber grabberScript = newgrabby.GetComponent<Grabber>();
        grabberScript.Init();
        //Debug.Log("spawned");
    }
}
