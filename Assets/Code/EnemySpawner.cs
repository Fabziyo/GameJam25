using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy-Prefab")]
    public GameObject[] enemyPrefab;

    [Header("Spawn Einstellungen")]
    public float spawnRate = 2f;      
    public float yMin = -2f;          
    public float yMax = 2f;           
    public Transform player;          

    private bool spawning = true;

    void Start()
    {
        //Spawn-Loop
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        if (!spawning) return;

        //Zufällige Position
        float randomY = Random.Range(yMin, yMax);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

        
        if (player != null)
            spawnPos.x = player.position.x + 15f;

        //random Enemy
        int randomIndex = Random.Range(0, enemyPrefab.Length);
        Instantiate(enemyPrefab[randomIndex], spawnPos, Quaternion.identity);
    }

    
    public void StopSpawning() { spawning = false; }
}
