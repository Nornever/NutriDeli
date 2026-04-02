using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    void Start()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopLoop();
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.PlayMusic(SoundManager.Instance.mainMenuMusic);
        }
    }
}