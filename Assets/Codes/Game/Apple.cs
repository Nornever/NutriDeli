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
        // Try to find CarControl in parent hierarchy
        CarControl car = other.GetComponentInParent<CarControl>();
        if (car != null)
        {
            // Optional: do something like give apples to PlayerStats
            PlayerStats stats = car.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.AddApples(1);
            }

            // Destroy the apple
            Destroy(gameObject);

            // Debug log to verify collection
            Debug.Log("Apple collected!");
        }
    }
}