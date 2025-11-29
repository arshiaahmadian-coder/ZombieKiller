using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    int score = 0;
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = "Coin: " + score.ToString();
    }

    public void SaveScoreIfIsNewRecord()
    {
        // TODO
    }
}
