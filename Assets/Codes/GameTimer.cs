using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Optional, if you want to load Win/Lose scenes

public class GameTimer : MonoBehaviour
{
    public float totalTime = 120f; // 2 minutes in seconds
    private float remainingTime;

    public Text timerText; // Drag a UI Text to show countdown

    public PlayerStats playerStats; // To check if player collected all apples

    void Start()
    {
        remainingTime = totalTime;
    }

    void Update()
    {
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            remainingTime = 0f;
            UpdateTimerUI();

            // Time is up! Check if player reached apple goal
            if (playerStats != null && playerStats.apples >= playerStats.appleGoal)
            {
                Debug.Log("You collected all apples in time! You win!");
                // SceneManager.LoadScene("WinScene"); // Uncomment to load Win scene
            }
            else
            {
                Debug.Log("Time's up! You failed to collect all apples.");
                // SceneManager.LoadScene("LoseScene"); // Optional lose scene
            }

            // Stop updating once time is up
            enabled = false;
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
    }
}