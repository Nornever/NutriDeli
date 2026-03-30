using UnityEngine;
using UnityEngine.UIElements;

public class Carrot : MonoBehaviour
{
    public float rotateSpeed = 90f;

    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime, 0f, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Try to find CarControl in parent hierarchy
        CarControl car = other.GetComponentInParent<CarControl>();
        if (car != null)
        {
            // Optional: do something like give carrots to PlayerStats
            PlayerStats stats = car.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.AddCarrots(1);
            }

            // Destroy the apple
            Destroy(gameObject);

            // Debug log to verify collection
            Debug.Log("Carrots collected!");
        }
    }
}