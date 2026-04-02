using UnityEngine;

public class Orange : MonoBehaviour
{
    public float rotateSpeed = 90f;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponentInParent<PlayerStats>();
        if (stats != null)
        {
            stats.CollectFruit("Orange");
            Destroy(gameObject);
            Debug.Log("Orange collected!");
        }
    }
}