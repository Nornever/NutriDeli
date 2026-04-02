using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public CarControl car; // assign your CarControl script here

    private bool enginePlaying = false;
    private bool brakePlaying = false;

    void Update()
    {
        if (SoundManager.Instance == null || car == null) return;

        // --- Engine Loop ---
        if (car.displayedSpeed > 0.1f) // moving forward
        {
            if (!enginePlaying)
            {
                SoundManager.Instance.PlayCarEngine();
                enginePlaying = true;
            }
        }
        else
        {
            if (enginePlaying)
            {
                SoundManager.Instance.StopCarEngine();
                enginePlaying = false;
            }
        }

        // --- Brake SFX ---
        bool isBraking = false;

        // Braking if decelerating or pressing space
        if ((car.currentThrottle < 0.1f && car.displayedSpeed > 0.1f) ||
            UnityEngine.InputSystem.Keyboard.current.spaceKey.isPressed)
        {
            isBraking = true;
        }

        // Play brake sound once per brake
        if (isBraking)
        {
            if (!brakePlaying)
            {
                SoundManager.Instance.PlayCarBrake();
                brakePlaying = true;
            }
        }
        else
        {
            brakePlaying = false;
        }
    }
}