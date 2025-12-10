using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScoreText.text = Player.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
