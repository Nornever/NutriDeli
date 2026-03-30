using UnityEngine;
using UnityEngine.UI; // For regular Text
// using TMPro; // Uncomment if using TextMeshPro

public class CarrotHUD : MonoBehaviour
{
    public PlayerStats playerStats; // Drag your Car (with PlayerStats) here
    public Text carrotText;          // Drag the UI Text here
    // public TMP_Text appleText;   // Use this line instead if using TextMeshPro

    void Update()
    {
        if (playerStats != null && carrotText != null)
        {
            carrotText.text = "Carrots: " + playerStats.carrots + " / " + playerStats.carrotsGoal;
        }
    }
}