using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI inputScore;
    
    //public static TextMeshProUGUI inputScore;
    public TextMeshProUGUI inputName;

    public UnityEvent<string, int> submitScoreEvent;
    public void SubmitScore()
    {
        if (!string.IsNullOrEmpty(inputName.text))
        {
            if (!string.IsNullOrWhiteSpace(inputName.text))
            {
                submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
            }
        }
        
            
        
    }
}
