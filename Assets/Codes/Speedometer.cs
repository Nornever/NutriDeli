using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public Rigidbody carRigidbody;   // Drag car here
    public RectTransform needle;     // Drag needle UI here

    public float maxSpeed = 120f;    // Max speed on dial (km/h)

    public float minAngle = -130f;   // Needle at 0 speed
    public float maxAngle = 130f;    // Needle at max speed

    void Update()
    {
        if (carRigidbody == null || needle == null) return;

        // Get speed in km/h
        float speed = carRigidbody.linearVelocity.magnitude * 3.6f;

        // Clamp speed
        float clampedSpeed = Mathf.Clamp(speed, 0, maxSpeed);

        // Convert speed to angle
        float t = clampedSpeed / maxSpeed;
        float angle = Mathf.Lerp(minAngle, maxAngle, t);

        // Apply rotation
        needle.localRotation = Quaternion.Euler(0, 0, angle);
    }
}