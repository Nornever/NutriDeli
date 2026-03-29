using UnityEngine;

public class jerryCan : MonoBehaviour
{
    public float rotateSpeed = 90f;

    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Look for CarControl in this object or any parent
        CarControl car = other.GetComponentInParent<CarControl>();
        if (car != null)
        {
            car.ActivateSpeedBoost(5f); // Boost 5 seconds
            Destroy(gameObject);
        }
    }
}