using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    private int scoreCount;

    void Start()
    {
        score.text = "0";
    }

    public void ScoreIncrease(int score)
    {
        scoreCount += score;
        this.score.text = scoreCount.ToString();
    }
}
