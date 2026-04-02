using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 120f;
    private float remainingTime;
    public Text timerText; // assign in inspector

    private bool timerRunning = false;

    void Start()
    {
        // Optional: don't start automatically
        // remainingTime = totalTime;
        // timerRunning = true;
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

    // Add this method so other scripts can start the timer
    public void StartTimer()
    {
        remainingTime = totalTime;
        timerRunning = true;
    }
}