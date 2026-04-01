using UnityEngine;

public class PathItemSpawner : MonoBehaviour
{
    [Header("Items")]
    public GameObject[] items;          // Apple, Orange, Carrot prefabs
    public LayerMask pathLayer;         // Layer for drivable path
    public float yOffset = 0.5f;       // Float above ground
    public float maxRandomDistance = 2f; // Random spread around checkpoint

    [Header("Path")]
    public Transform pathParent;        // Your "Path" GameObject with 10 child points

    void Start()
    {
        SpawnItemsAlongPath();
    }

    void SpawnItemsAlongPath()
    {
        if (pathParent == null || pathParent.childCount == 0) return;

        // Loop through all path points (checkpoints)
        foreach (Transform checkpoint in pathParent)
        {
            // Spawn all items (apple, orange, carrot) near this checkpoint
            foreach (GameObject itemPrefab in items)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-maxRandomDistance, maxRandomDistance),
                    0f,
                    Random.Range(-maxRandomDistance, maxRandomDistance)
                );

                Vector3 spawnPos = checkpoint.position + randomOffset;
                spawnPos.y += 10f; // Start raycast above path

                // Raycast down to the path to get correct Y
                if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, 20f, pathLayer))
                {
                    spawnPos.y = hit.point.y + yOffset;

                    // Align item to the slope of the terrain
                    Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                    Instantiate(itemPrefab, spawnPos, slopeRotation);
                }
            }
        }
    }
}