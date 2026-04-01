using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameInputSceneManager : MonoBehaviour
{
    public InputField nameInputField; // assign in inspector

    void Start()
    {
        // Pre-fill with saved name if exists
        if (PlayerPrefs.HasKey("PlayerName"))
            nameInputField.text = PlayerPrefs.GetString("PlayerName");
    }

    public void StartGame()
    {
        string playerName = nameInputField.text;

        if (string.IsNullOrEmpty(playerName))
            playerName = "Player";

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        // Load your main game scene
        SceneManager.LoadScene("SampleScene"); // replace with actual game scene name
    }
}