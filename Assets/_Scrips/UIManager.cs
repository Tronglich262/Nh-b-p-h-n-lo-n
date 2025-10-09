// UIManager.cs – ví dụ đơn giản
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text timerTxt, scoreTxt, coinsTxt;
    public UnityEngine.UI.Image recipeIcon;

    float timeLeft = 60f;
    int score = 0, coins = 0;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) timeLeft = 0;
        timerTxt.text = $"{Mathf.FloorToInt(timeLeft / 60):0}:{Mathf.FloorToInt(timeLeft % 60):00}";
    }

    public void AddScore(int v) { score += v; scoreTxt.text = score.ToString(); }
    public void AddCoins(int v) { coins += v; coinsTxt.text = coins.ToString(); }
    public void SetRecipeIcon(Sprite s) { if (recipeIcon) recipeIcon.sprite = s; }
    public void ShowRecipeComplete() { /* popup/âm thanh tuỳ bạn */ }
}
