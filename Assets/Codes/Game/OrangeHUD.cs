using UnityEngine;
using UnityEngine.UI; // For regular Text
// using TMPro; // Uncomment if using TextMeshPro

public class OrangeHUD : MonoBehaviour
{
    public PlayerStats playerStats; // Drag your Car (with PlayerStats) here
    public Text orangeText;          // Drag the UI Text here
    // public TMP_Text appleText;   // Use this line instead if using TextMeshPro

    void Update()
    {
        if (playerStats != null && orangeText != null)
        {
            orangeText.text = "Oranges: " + playerStats.oranges + " / " + playerStats.orangesGoal;
        }
    }
}