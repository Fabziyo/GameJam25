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

    [Header("Cleanup Einstellungen")]
    public string enemyTag = "Enemy";
    public float destroyDistance = 5f;

    private bool spawning = true;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
    }

    void Update()
    {
        if (player == null) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float playerX = player.position.x;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

            if (enemy.transform.position.x < playerX - destroyDistance)
            {
                Destroy(enemy);
            }
        }
    }

    void SpawnEnemy()
    {
        if (!spawning || player == null) return;
       
        float randomOffsetY = Random.Range(yMin, yMax);

        Vector3 spawnPos = new Vector3(
            player.position.x + 15f,         
            player.position.y + randomOffsetY,
            0f
        );

        int randomIndex = Random.Range(0, enemyPrefab.Length); 
        Instantiate(enemyPrefab[randomIndex], spawnPos, Quaternion.identity);
    }
    public void StopSpawning()
    {
        spawning = false;
    }
}
