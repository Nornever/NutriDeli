using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 120f; // 2 minutes
    private float remainingTime;

    public Text timerText;          // Assign your legacy Text UI
    public PlayerStats playerStats; // Reference to truck's PlayerStats

    private bool timerRunning = false;

    void Start()
    {
        StartTimer(); // Automatically start timer
    }

    void Update()
    {
        if (!timerRunning) return;

        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            remainingTime = 0f;
            UpdateTimerUI();
            timerRunning = false;
            EndGame();
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }

    public void StartTimer()
    {
        remainingTime = totalTime;
        timerRunning = true;
    }

    void EndGame()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats not assigned in GameTimer!");
            return;
        }

        // Save collected fruits to GameData
        GameData.Instance.applesPicked = playerStats.apples;
        GameData.Instance.carrotsPicked = playerStats.carrots;
        GameData.Instance.orangesPicked = playerStats.oranges;

        // Save player name (from NameInput scene)
        GameData.Instance.playerName = PlayerPrefs.GetString("PlayerName", "Player");

        // Add current player to leaderboard
        GameData.Instance.AddCurrentPlayerToLeaderboard();

        // Load the Leaderboard scene automatically
        SceneManager.LoadScene("LeaderboardScene"); // <-- exact name match!
    }
}