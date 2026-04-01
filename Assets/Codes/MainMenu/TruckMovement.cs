using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    public float baseSpeed = 10f; // base speed at orthographic size 10
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Adjust speed based on camera orthographic size
        float adjustedSpeed = baseSpeed * (mainCamera.orthographicSize / 10f);

        // Move right
        transform.Translate(Vector3.right * adjustedSpeed * Time.deltaTime);
    }
}