using UnityEngine;

public class HindernisScript : MonoBehaviour
{
    [Header("Hindernis-Prefab")]
    public GameObject[] HindernisPrefab;

    [Header("Spawn Einstellungen")]
    public float spawnRate = 2f;
    public float yMin = -2f;
    public float yMax = 2f;
    public Transform player;

    private bool spawning = true;

    void Start()
    {
        //Spawn-Loop
        InvokeRepeating("SpawnHindernis", 1f, spawnRate);
    }

    void SpawnHindernis()
    {
        if (!spawning) return;

        //Zufällige Position
        float randomY = Random.Range(yMin, yMax);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);


        if (player != null)
            spawnPos.x = player.position.x + 15f;

        //random Hindernis
        int randomIndex = Random.Range(0, HindernisPrefab.Length);
        Instantiate(HindernisPrefab[randomIndex], spawnPos, Quaternion.identity);
    }


    public void StopSpawning() { spawning = false; }
}
