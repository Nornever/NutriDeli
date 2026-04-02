using UnityEngine;
using System.Collections.Generic;

public class DynamicPathItemSpawner : MonoBehaviour
{
    [Header("Items")]
    public GameObject[] items;              // Apple, Orange, Carrot prefabs
    public LayerMask pathLayer;             // Layer for drivable path
    public float yOffset = 0.5f;            // Float above terrain
    public float maxRandomDistance = 2f;    // Lateral random offset

    [Header("Path")]
    public Transform pathParent;            // Parent with checkpoint children

    [Header("Player")]
    public Transform player;                // Truck/player
    public float spawnDistanceAhead = 80f;  // How far ahead to spawn fruits
    public float despawnDistanceBehind = 10f; // Distance to remove fruits behind player

    [Header("Fruit Count")]
    public int totalPerType = 10;           // Max 10 of each fruit type

    [Header("Movement Axis")]
    public bool useXAxis = false;           // true if car moves along X, false = Z

    private List<Transform> checkpoints = new List<Transform>();
    private Dictionary<GameObject, int> spawnedCount = new Dictionary<GameObject, int>();
    private List<GameObject> activeFruits = new List<GameObject>();

    void Start()
    {
        // Initialize counts
        foreach (GameObject item in items)
            spawnedCount[item] = 0;

        // Collect checkpoints
        foreach (Transform child in pathParent)
            checkpoints.Add(child);
    }

    void Update()
    {
        CleanupNullFruits();   // Remove destroyed fruits from list
        SpawnFruitsAhead();
        DespawnFruitsBehind();
    }

    void CleanupNullFruits()
    {
        for (int i = activeFruits.Count - 1; i >= 0; i--)
        {
            if (activeFruits[i] == null)
                activeFruits.RemoveAt(i);
        }
    }

    void SpawnFruitsAhead()
    {
        foreach (Transform checkpoint in checkpoints)
        {
            float checkpointPos = useXAxis ? checkpoint.position.x : checkpoint.position.z;
            float playerPos = useXAxis ? player.position.x : player.position.z;

            // Only spawn if checkpoint is ahead and within spawn distance
            if (checkpointPos > playerPos && checkpointPos < playerPos + spawnDistanceAhead)
            {
                // Check if checkpoint already has fruit nearby
                bool hasFruitNearby = false;
                for (int i = activeFruits.Count - 1; i >= 0; i--)
                {
                    GameObject f = activeFruits[i];
                    if (f == null)
                    {
                        activeFruits.RemoveAt(i); // Cleanup destroyed fruits
                        continue;
                    }

                    if (Vector3.Distance(f.transform.position, checkpoint.position) < 5f)
                    {
                        hasFruitNearby = true;
                        break;
                    }
                }
                if (hasFruitNearby) continue;

                // Spawn 1 or 2 fruits at this checkpoint
                int fruitsToSpawn = Random.Range(1, 3);

                for (int i = 0; i < fruitsToSpawn; i++)
                {
                    // Pick random fruit type with remaining quota
                    List<GameObject> availableItems = new List<GameObject>();
                    foreach (GameObject item in items)
                        if (spawnedCount[item] < totalPerType)
                            availableItems.Add(item);

                    if (availableItems.Count == 0) return; // All fruits spawned

                    GameObject chosenItem = availableItems[Random.Range(0, availableItems.Count)];

                    // Random lateral offset
                    Vector3 randomOffset = new Vector3(
                        Random.Range(-maxRandomDistance, maxRandomDistance),
                        50f,  // start raycast above terrain
                        Random.Range(-maxRandomDistance, maxRandomDistance)
                    );

                    Vector3 spawnPos = checkpoint.position + randomOffset;

                    // Raycast down to path layer
                    if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, 100f, pathLayer))
                    {
                        spawnPos.y = hit.point.y + yOffset;
                        Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                        GameObject fruit = Instantiate(chosenItem, spawnPos, slopeRotation);
                        activeFruits.Add(fruit);
                        spawnedCount[chosenItem]++;
                    }
                }
            }
        }
    }

    void DespawnFruitsBehind()
    {
        float playerPos = useXAxis ? player.position.x : player.position.z;

        for (int i = activeFruits.Count - 1; i >= 0; i--)
        {
            GameObject fruit = activeFruits[i];
            if (fruit == null)
            {
                activeFruits.RemoveAt(i);
                continue;
            }

            float fruitPos = useXAxis ? fruit.transform.position.x : fruit.transform.position.z;
            if (fruitPos < playerPos - despawnDistanceBehind)
            {
                Destroy(fruit);
                activeFruits.RemoveAt(i);
            }
        }
    }
}