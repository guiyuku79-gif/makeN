using UnityEngine;
using unityroom.Api;

public static class SaveData
{
    private const string SolvedCountKey = "SolvedCount";

    public static int SolvedCount
    {
        get => PlayerPrefs.GetInt(SolvedCountKey, 0);
        set
        {
            PlayerPrefs.SetInt(SolvedCountKey, value);
            PlayerPrefs.Save();
        }
    }

    public static void AddSolvedCount()
    {
        SolvedCount++;
        UnityroomApiClient.Instance.SendScore(1, SolvedCount, ScoreboardWriteMode.HighScoreDesc);
    }
}