using UnityEngine;
using TMPro;
using System.Collections;

public class GameManagerCountdown : MonoBehaviour
{
    public TMP_Text countdownText;     // TMP text for countdown
    public float countdownTime = 10f;

    public CarControl car;             // Reference to your Car
    public GameTimer gameTimer;        // Reference to your existing GameTimer

    void Start()
    {
        if (car != null) car.canMove = false;
        if (gameTimer != null) gameTimer.enabled = false;

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

        if (car != null) car.canMove = true;
        if (gameTimer != null) gameTimer.StartTimer();
    }
}