using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int apples = 0;           // Number of apples collected
    public int appleGoal = 10;       // Goal to win

    public void AddApples(int amount)
    {
        apples += amount;
        Debug.Log("Apples: " + apples);

        if (apples >= appleGoal)
        {
            Debug.Log("Apple goal reached! You win!");
            // Optional: Load Win Scene here
            // UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
        }
    }
}