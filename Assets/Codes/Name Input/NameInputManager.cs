using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameInputManager : MonoBehaviour
{
    public InputField nameInputField; // assign in inspector
    public string gameSceneName = "GameScene"; // gameplay scene name

    public void OnSubmitName()
    {
        string enteredName = nameInputField.text.Trim();
        if (string.IsNullOrEmpty(enteredName))
            enteredName = "Player";

        // save for current session & PlayerPrefs
        PlayerPrefs.SetString("PlayerName", enteredName);
        PlayerPrefs.Save();

        if (GameData.Instance != null)
            GameData.Instance.playerName = enteredName;

        Debug.Log("Player name set to: " + enteredName);

        SceneManager.LoadScene("SampleScene");
    }
}