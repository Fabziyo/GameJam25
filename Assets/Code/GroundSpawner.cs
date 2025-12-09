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

    public bool spawning = true;
    public float worldY;

    void Start()
    {
        
        InvokeRepeating("SpawnGround", 1f, spawnRate);
    }

    void SpawnGround()
    {
        if (!spawning || player == null) return;

        
        Vector3 playerGridPos = GetGridPosition(player.position);
        Vector3 nextSpawnGridPos = new Vector3(playerGridPos.x + gridSizeX, playerGridPos.y, 0);

        
        if (IsGridPositionFree(nextSpawnGridPos))
        {
            
            float randomY = Mathf.Clamp(Random.Range(yMin, yMax),
                                       nextSpawnGridPos.y - gridSizeY / 2,
                                       nextSpawnGridPos.y + gridSizeY / 2);

            Vector3 spawnPos = new Vector3(nextSpawnGridPos.x, randomY, 0);

            
            int randomIndex = Random.Range(0, GroundPrefab.Length);
            Instantiate(GroundPrefab[randomIndex], spawnPos, Quaternion.identity);
        }
    }

    Vector3 GetGridPosition(Vector3 worldPos)
    {
        
        float gridX = Mathf.Floor(worldPos.x / gridSizeX) * gridSizeX;
        float gridY = Mathf.Round(worldY / gridSizeY) * gridSizeY;
        return new Vector3(gridX, gridY, 0);
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
