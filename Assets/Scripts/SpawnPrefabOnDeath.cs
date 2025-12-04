using UnityEngine;

public class SpawnPrefabOnDeath : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;

    public void Spawn ()
    {
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
}
