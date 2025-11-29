using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    int coins = 0;
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void AddCoin(int scoreAmount)
    {
        coins += scoreAmount;
        scoreText.text = "Coin: " + coins.ToString();
    }

    public void SpendCoin(int spendAmount)
    {
        coins -= spendAmount;
        scoreText.text = "Coin: " + coins.ToString();
    }

    public bool CanBuy(int price)
    {
        return coins - price >= 0;
    }
}
