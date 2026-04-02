using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Countdown TMP Settings")]
    public TMP_Text countdownText;     // 10-second countdown
    public float countdownTime = 3f;   // use 3s for testing

    [Header("Car Settings")]
    public GameObject car;             // assign your car GameObject
    private PlayerStats playerStats;
    private CarControl carControl;

    [Header("Gameplay Timer Settings")]
    public Text timerText;             // legacy UI Text
    public float totalTime = 120f;     // 2 minutes
    private float remainingTime;
    private bool timerRunning = false;

    void Start()
    {
        if (car != null)
        {
            playerStats = car.GetComponent<PlayerStats>();
            carControl = car.GetComponent<CarControl>();

            if (carControl != null)
                carControl.canMove = false; // disable movement until countdown ends
        }

        remainingTime = totalTime;

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

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

        // Enable car movement after countdown
        if (carControl != null)
            carControl.canMove = true;

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
            EndGame();
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

    void EndGame()
    {
        Debug.Log("Time's up! Ending game.");

        if (playerStats != null && GameData.Instance != null)
        {
            GameData.Instance.applesPicked = playerStats.apples;
            GameData.Instance.carrotsPicked = playerStats.carrots;
            GameData.Instance.orangesPicked = playerStats.oranges;

            // ensure current player name is up to date
            GameData.Instance.playerName = PlayerPrefs.GetString("PlayerName", "Player");

            GameData.Instance.AddCurrentPlayerToLeaderboard();
        }

        SceneManager.LoadScene("LeaderboardScene");
    }
}