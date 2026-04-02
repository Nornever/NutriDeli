using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Fruit Counters")]
    public int apples = 0;
    public int carrots = 0;
    public int oranges = 0;

    [Header("Fruit Goals")]
    public int appleGoal = 10;
    public int carrotsGoal = 10;
    public int orangesGoal = 10;

    [HideInInspector] public float lastFruitCollectedTime = 0f;
    private float gameStartTime;

    void Start()
    {
        gameStartTime = Time.time;
    }

    public void CollectFruit(string fruitType)
    {
        float elapsed = Time.time - gameStartTime;

        switch (fruitType.ToLower())
        {
            case "apple":
                if (apples < appleGoal) apples++;
                break;
            case "carrot":
                if (carrots < carrotsGoal) carrots++;
                break;
            case "orange":
                if (oranges < orangesGoal) oranges++;
                break;
        }

        lastFruitCollectedTime = elapsed;

        // Debug info
        Debug.Log($"Collected {fruitType}. Total: Apples={apples}, Carrots={carrots}, Oranges={oranges}");

        if (apples >= appleGoal && carrots >= carrotsGoal && oranges >= orangesGoal)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        PlayerPrefs.SetString("LastPlayerName", playerName);
        PlayerPrefs.SetFloat("LastPlayerTime", lastFruitCollectedTime);
        PlayerPrefs.Save();

        SceneManager.LoadScene("LeaderboardScene");
    }
}