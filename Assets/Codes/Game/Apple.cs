using UnityEngine;

public class Apple : MonoBehaviour
{
    public float rotateSpeed = 90f;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the truck
        PlayerStats stats = other.GetComponentInParent<PlayerStats>();
        if (stats != null)
        {
            stats.CollectFruit("Apple"); // Collect fruit
            Destroy(gameObject);          // Remove apple from scene
            Debug.Log("Apple collected!");
        }
    }
}