using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{

    [SerializeField] GameObject grab;
    [SerializeField] GameObject[] wires;

    [SerializeField] float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 3f;

    [SerializeField] Vector2 xRange = new Vector2(-5f, 5f);
    [SerializeField] Vector2 yRange = new Vector2(-3f, 3f);

    Animator anim;
    bool startedSpawning = false;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!startedSpawning && !AllWiresGone())
        {
            startedSpawning = true;

            StartCoroutine(SpawnLoop());
        }

        
    }

    bool AllWiresGone()
    {
        foreach (var wire in wires)
        {
            if (wire != null) return false;
        }

        return true;
    }
    
    IEnumerator SpawnGrabber()
    {
        yield return null;

        Vector2 spawnPos = new Vector2(Random.Range(xRange.x, xRange.y),Random.Range(yRange.x, yRange.y));

        GameObject newgrabby = Instantiate(grab, spawnPos, Quaternion.identity);

        //Debug.Log("spawned");
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            StartCoroutine(SpawnGrabber());
        }
    }
}
