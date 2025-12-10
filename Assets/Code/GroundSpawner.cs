using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Ground-Prefab")]
    public GameObject[] GroundPrefab;

    [Header("Spawn Einstellungen")]
    public float spawnRate = 2f;
    public float yMin = -2f;
    public float yMax = 2f;
    public Transform player;

    [Header("Grid Einstellungen")]
    public float gridSizeX = 10f;
    public float gridSizeY = 4f;
    public LayerMask groundLayer = 1;  

    [Header("Cleanup Einstellungen")]
    public string groundTag = "Ground";   
    public float destroyDistance = 10f;

    private bool spawning = true;
    private float nextSpawnX;

    void Start()
    {
        nextSpawnX = transform.position.x;
        InvokeRepeating(nameof(SpawnGround), 1f, spawnRate);
    }

    void Update()
    {
        if (player == null) return;

        GameObject[] grounds = GameObject.FindGameObjectsWithTag(groundTag);
        float playerX = player.position.x;

        foreach (GameObject ground in grounds)
        {
            if (ground == null) continue;

            if (ground.transform.position.x < playerX - destroyDistance)
            {
                Destroy(ground);
                
            }
        }
    }

    void SpawnGround()
    {
        if (!spawning) return;

        Vector3 nextSpawnGridPos = new Vector3(nextSpawnX, transform.position.y, 0);

        if (player != null)
        {
            nextSpawnGridPos.x = Mathf.Max(nextSpawnGridPos.x, player.position.x + 15f);
        }

        if (IsGridPositionFree(nextSpawnGridPos))
        {
            float randomY = Mathf.Clamp(
                Random.Range(yMin, yMax),
                nextSpawnGridPos.y - gridSizeY / 2,
                nextSpawnGridPos.y + gridSizeY / 2
            );

            Vector3 spawnPos = new Vector3(nextSpawnGridPos.x, randomY, 0);

            int randomIndex = Random.Range(0, GroundPrefab.Length);
            Instantiate(GroundPrefab[randomIndex], spawnPos, Quaternion.identity);

            nextSpawnX += gridSizeX;
        }
        else
        {
            nextSpawnX += gridSizeX;
        }
    }

    bool IsGridPositionFree(Vector3 gridPos)
    {
        Vector3 boxCenter = gridPos;
        Vector3 boxSize = new Vector3(gridSizeX * 0.8f, gridSizeY * 0.8f, 1f);

        Collider[] overlaps = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.identity, groundLayer);
        return overlaps.Length == 0;
    }

    public void StopSpawning() { spawning = false; }
}
