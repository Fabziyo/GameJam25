using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public TextMeshProUGUI playerScore;
    private int score;

    private void Start()
    {
        score = 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            score++;
            playerScore.text = score.ToString();
        }
    }
}
