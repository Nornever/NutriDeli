using UnityEngine;
using UnityEngine.UI; // For regular Text
// using TMPro; // Uncomment if using TextMeshPro

public class AppleHUD : MonoBehaviour
{
    public PlayerStats playerStats; // Drag your Car (with PlayerStats) here
    public Text appleText;          // Drag the UI Text here
    // public TMP_Text appleText;   // Use this line instead if using TextMeshPro

    void Update()
    {
        if (playerStats != null && appleText != null)
        {
            appleText.text = "Apples: " + playerStats.apples + " / " + playerStats.appleGoal;
        }
    }
}