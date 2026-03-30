using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int apples = 0;           // Number of apples collected
    public int carrots = 0;
    public int oranges = 0;
    public int appleGoal = 10;       // Goal to win
    public int carrotsGoal = 10;
    public int orangesGoal = 10;

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
    public void AddCarrots(int amount)
    {
        carrots += amount;
        Debug.Log("Carrots: " + carrots);

        if (carrots >= carrotsGoal)
        {
            Debug.Log("Carrots goal reached! You win!");
            // Optional: Load Win Scene here
            // UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
        }
    }
    public void AddOranges(int amount)
    {
        oranges += amount;
        Debug.Log("Oranges: " + oranges);

        if (oranges >= orangesGoal)
        {
            Debug.Log("Orange goal reached! You win!");
            // Optional: Load Win Scene here
            // UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
        }
    }
}