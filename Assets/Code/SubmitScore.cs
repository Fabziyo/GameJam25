using LeaderboardCreatorDemo;
using TMPro;
using UnityEngine;

public class SubmitScore : MonoBehaviour
{
    public LeaderboardManager leaderboard;
    public void UserScoreUpload()
    {
        leaderboard.UploadEntry();
    }
}
