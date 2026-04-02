using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    public Text resultText;      // Shows current player fruit count
    public Text leaderboardText; // Shows ranking + player names only
    public int maxEntries = 5;   // Keep last 5 players

    void Start()
    {
        DisplayResult();
        DisplayLeaderboard();
    }

    void DisplayResult()
    {
        if (GameData.Instance == null || GameData.Instance.leaderboardEntries.Count == 0)
        {
            if (resultText != null)
                resultText.text = "Your Result: 0 fruits";
            return;
        }

        // Get latest player (last added)
        var currentPlayer = GameData.Instance.leaderboardEntries[
            GameData.Instance.leaderboardEntries.Count - 1
        ];

        int totalFruits = currentPlayer.apples + currentPlayer.carrots + currentPlayer.oranges;

        if (resultText != null)
            resultText.text = $"You collected {totalFruits} fruits " +
                              $"(Apples:{currentPlayer.apples}, Carrots:{currentPlayer.carrots}, Oranges:{currentPlayer.oranges})";
    }

    void DisplayLeaderboard()
    {
        if (GameData.Instance == null || GameData.Instance.leaderboardEntries.Count == 0)
        {
            if (leaderboardText != null)
                leaderboardText.text = "Leaderboard N/A";
            return;
        }

        // Create a copy so original order is untouched
        List<LeaderboardEntry> sortedList = new List<LeaderboardEntry>(GameData.Instance.leaderboardEntries);

        // Sort by total fruits (highest first)
        sortedList.Sort((a, b) =>
        {
            int totalA = a.apples + a.carrots + a.oranges;
            int totalB = b.apples + b.carrots + b.oranges;
            return totalB.CompareTo(totalA);
        });

        string board = "";
        int entriesToShow = Mathf.Min(maxEntries, sortedList.Count);

        for (int i = 0; i < entriesToShow; i++)
        {
            var e = sortedList[i];
            board += $"{i + 1}. {e.playerName}\n";
        }

        if (leaderboardText != null)
            leaderboardText.text = board;
    }

    void Update()
    {
        // Press Z to clear leaderboard (for testing)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (GameData.Instance != null)
            {
                GameData.Instance.ClearLeaderboard();
                Debug.Log("Leaderboard cleared!");
            }
        }
    }
}