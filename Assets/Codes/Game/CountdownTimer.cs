using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText; // assign in inspector (TMP or Legacy)
    public float countdownTime = 10f; // 10 seconds countdown

    public GameTimer gameTimer; // assign the GameTimer script here

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        float currentTime = countdownTime;

        while (currentTime > 0f)
        {
            countdownText.text = Mathf.Ceil(currentTime).ToString();
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
        }

        // Hide countdown text after 1
        countdownText.text = "";
        countdownText.gameObject.SetActive(false);

        // Start the main game timer
        if (gameTimer != null)
            gameTimer.StartTimer();
    }
}