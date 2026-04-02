using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;        // background music
    public AudioSource loopSource;         // looping SFX (engine, countdown)
    public AudioSource effectsSource;      // one-shot SFX (click, brake, collect)

    [Header("Audio Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip buttonClick;
    public AudioClip countdownBeep;
    public AudioClip carEngine;
    public AudioClip carBrake;
    public AudioClip collectItem;

    void Awake()
    {
        Instance = this; // simple reference, no persistence
    }

    // --- Music ---
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // --- Looping SFX ---
    public void PlayLoop(AudioClip clip)
    {
        if (loopSource.clip != clip || !loopSource.isPlaying)
        {
            loopSource.clip = clip;
            loopSource.loop = true;
            loopSource.Play();
        }
    }

    public void StopLoop()
    {
        loopSource.Stop();
    }

    // --- One-shot SFX ---
    public void PlayButtonClick() => effectsSource.PlayOneShot(buttonClick);
    public void PlayCountdownBeep() => effectsSource.PlayOneShot(countdownBeep);
    public void PlayCarBrake() => effectsSource.PlayOneShot(carBrake);
    public void PlayCollectItem() => effectsSource.PlayOneShot(collectItem);

    // --- Engine control ---
    public void PlayCarEngine() => PlayLoop(carEngine);
    public void StopCarEngine() => StopLoop();
}