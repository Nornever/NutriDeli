using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 120f;   // 2 minutes
    private float remainingTime;
    public Text timerText;            // Drag Timer Text here

    private bool timerRunning = false;

    void Start()
    {
        remainingTime = totalTime;
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
            Debug.Log("Time's up!");
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
    }
}