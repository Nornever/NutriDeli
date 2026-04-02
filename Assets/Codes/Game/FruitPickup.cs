using UnityEngine;

public class FruitPickup : MonoBehaviour
{
    public string fruitType; // "Apple", "Carrot", "Orange"
    private PlayerStats playerStats;
    private FruitAudio fruitAudio;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        fruitAudio = FindObjectOfType<FruitAudio>();

        if (fruitAudio == null)
            Debug.LogError("FruitAudio not found in scene!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats.CollectFruit(fruitType);

            if (fruitAudio != null)
                fruitAudio.PlayCollect();

            Destroy(gameObject);
        }
    }
}