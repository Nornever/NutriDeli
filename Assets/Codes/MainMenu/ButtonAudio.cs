using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    // Plays button click sound
    public void PlayClick()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayButtonClick();
    }

    // Stops the main menu music
    public void StopMusic()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.musicSource.Stop();
    }
}