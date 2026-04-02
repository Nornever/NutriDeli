using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownAudio : MonoBehaviour
{
    public TMP_Text countdownText;  // UI Text for countdown
    public float countdownTime = 3f;

    void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        float counter = countdownTime;

        while (counter > 0)
        {
            if (countdownText != null)
                countdownText.text = counter.ToString("0");

            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayCountdownBeep();

            yield return new WaitForSeconds(1f);
            counter--;
        }

        if (countdownText != null)
            countdownText.text = "GO!";

        yield return new WaitForSeconds(1f);
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);
    }
}