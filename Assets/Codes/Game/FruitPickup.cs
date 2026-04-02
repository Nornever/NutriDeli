using UnityEngine;

public class FruitPickup : MonoBehaviour
{
    public string fruitType; // "Apple", "Carrot", "Orange"
    private PlayerStats playerStats;

    void Start()
    {
        if (playerStats == null)
            playerStats = FindObjectOfType<PlayerStats>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats.CollectFruit(fruitType);
            Destroy(gameObject);
        }
    }
}