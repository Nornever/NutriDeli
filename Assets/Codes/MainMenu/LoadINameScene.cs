using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("NameInput"); // replace with your scene name
    }
}