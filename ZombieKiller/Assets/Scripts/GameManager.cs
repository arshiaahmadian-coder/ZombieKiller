using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject gameOverCanvas;
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

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        FindFirstObjectByType<PlayerCam>().DisableLook();
        FindFirstObjectByType<PlayerMovement>().DisableMovement();
    }

    public void RestartGme()
    {
        // load current scene (reload scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
