using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int apples;
    public int carrots;
    public int oranges;
}

// Wrapper class for JSON serialization
[System.Serializable]
public class LeaderboardWrapper
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
    public string playerName;
    public int applesPicked;
    public int carrotsPicked;
    public int orangesPicked;
    public void ClearLeaderboard()
    {
        leaderboardEntries.Clear();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLeaderboard();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCurrentPlayerToLeaderboard()
    {
        LeaderboardEntry entry = new LeaderboardEntry
        {
            playerName = playerName,
            apples = applesPicked,
            carrots = carrotsPicked,
            oranges = orangesPicked
        };

        leaderboardEntries.Insert(0, entry); // latest on top

        if (leaderboardEntries.Count > 5)
            leaderboardEntries.RemoveAt(leaderboardEntries.Count - 1);

        SaveLeaderboard();
    }

    void SaveLeaderboard()
    {
        LeaderboardWrapper wrapper = new LeaderboardWrapper();
        wrapper.entries = leaderboardEntries;

        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("LeaderboardData", json);
        PlayerPrefs.Save();
    }

    void LoadLeaderboard()
    {
        if (PlayerPrefs.HasKey("LeaderboardData"))
        {
            string json = PlayerPrefs.GetString("LeaderboardData");
            LeaderboardWrapper wrapper = JsonUtility.FromJson<LeaderboardWrapper>(json);
            if (wrapper != null && wrapper.entries != null)
                leaderboardEntries = wrapper.entries;
        }
    }
}