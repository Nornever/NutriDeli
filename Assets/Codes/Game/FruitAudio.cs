using UnityEngine;

public class FruitAudio : MonoBehaviour
{
    public AudioSource pickupSource;   // assign AudioSource in inspector
    public AudioClip collectClip;      // assign fruit collect clip

    public void PlayCollect()
    {
        if (pickupSource != null && collectClip != null)
        {
            pickupSource.PlayOneShot(collectClip);
        }
        else
        {
            Debug.LogWarning("PickupSource or CollectClip not assigned!");
        }
    }
}