using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardUI : MonoBehaviour
{
    // Call this function in the button OnClick
    public void BackToMainMenu()
    {
        // Stop any looping game sounds (car engine, countdown, etc.)
        if (SoundManager.Instance != null)
            SoundManager.Instance.StopLoop();  // <-- only stops looped SFX, not music

        // Load the Main Menu scene
        SceneManager.LoadScene("Main Lobby Scene");
    }
}