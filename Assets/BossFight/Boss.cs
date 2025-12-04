using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{

    [SerializeField] GameObject grab;
    [SerializeField] GameObject[] wires;

    [SerializeField] float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 3f;

    [SerializeField] Vector2 xRange = new Vector2(-5f, 5f);
    [SerializeField] Vector2 yRange = new Vector2(-3f, 3f);

    public Dialogue beforeBossDialogue;
    public Dialogue afterBossDialogue;

    Animator anim;
    bool startedSpawning = false;

    [SerializeField] UnityEvent onAllWiresGone;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!startedSpawning && !AllWiresGone())
        {
            startedSpawning = true;
            
            TriggerDialogue(beforeBossDialogue);

            StartCoroutine(SpawnLoop());
        }

        if (AllWiresGone())
        {
            if (onAllWiresGone != null)
            {
                onAllWiresGone.Invoke();
            }
        }
    }

    bool AllWiresGone()
    {
        foreach (var wire in wires)
        {
            if (wire != null) return false;
        }

        TriggerDialogue(afterBossDialogue);

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

    public void TriggerDialogue(Dialogue dialogue)
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void Next()
    {
        DialogueManager.Instance.SpeedUp();
    }
}
