using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Countdown TMP Settings")]
    public TMP_Text countdownText;     // 10-second countdown
    public float countdownTime = 10f;

    [Header("Car Settings")]
    public CarControl car;             // Your car reference

    [Header("Legacy Timer Settings")]
    public Text timerText;             // Legacy UI Text for 2-minute timer
    public float totalTime = 120f;     // 2 minutes in seconds

    private float remainingTime;
    private bool timerRunning = false;

    void Start()
    {
        // Disable car and legacy timer at start
        if (car != null) car.canMove = false;

        if (timerText != null)
            timerText.text = "02:00"; // initial display

        remainingTime = totalTime;

        // Start the countdown coroutine
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float counter = countdownTime;

        while (counter > 0)
        {
            if (countdownText != null)
                countdownText.text = counter.ToString("0");

            yield return new WaitForSeconds(1f);
            counter--;
        }

        if (countdownText != null)
            countdownText.text = "GO!";

        yield return new WaitForSeconds(1f);

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        // Unlock car and start timer
        if (car != null) car.canMove = true;
        timerRunning = true;
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
            // You can trigger end-of-game logic here
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
}