using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float levelTime = 60f;
    private float timer;
    public int score = 0;
    public int coins = 0;
    public int lives = 3;

    [Header("UI")]
    public Text timerText;
    public Text scoreText;
    public Text coinsText;
    public GameObject resultPanel;

    void Start()
    {
        timer = levelTime;
        UpdateHUD();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "" + Mathf.CeilToInt(timer).ToString();

        if (timer <= 0 || lives <= 0)
        {
            EndGame();
        }
    }

    public void OnRecipeCompleted()
    {
        score += 100;
        coins += 50;
        UpdateHUD();
    }

    public void LoseLife()
    {
        lives--;
        UpdateHUD();
    }

    void UpdateHUD()
    {
        scoreText.text = " " + score;
        coinsText.text = " " + coins;
    }

    void EndGame()
    {
        Time.timeScale = 0;
        resultPanel.SetActive(true);
    }
}
