using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreCounterBehavior : MonoBehaviour
{
    [SerializeField] private GameStateChannel gameStateChannel;
    [SerializeField] private SingleUniversalValue highscore;
    private TextMeshProUGUI scoreText;
    public int score = 0;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if (highscore.IntValue > 0)
        {
            scoreText.enabled = true;
            scoreText.text = ("High score: " + highscore.IntValue.ToString());
        }
        else
        {
            scoreText.enabled = false;
        }
    }
    private void OnEnable()
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnStart += OnStart;
        gameStateChannel.OnRestart += OnRestart;
        gameStateChannel.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnStart -= OnStart;
        gameStateChannel.OnRestart -= OnRestart;
        gameStateChannel.OnDeath -= OnDeath;
    }
    private void OnStart()
    {
        score = 0;
        scoreText.enabled = true;
        StartCoroutine(ScoreCounter());
    }

    private void OnDeath()
    {
        StopAllCoroutines();
    }

    private void OnRestart()
    {
        highscore.IntValue = Mathf.Max(highscore.IntValue, score);
        scoreText.text = ("High score: " + highscore.IntValue.ToString());
    } 
    IEnumerator ScoreCounter()
    {
        while (true)
        {
            scoreText.text = score.ToString();
            yield return new WaitForSeconds(1f);
            score++;
        }
    }
}
