using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreCounterBehavior : MonoBehaviour
{
    public int score = 0;
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        score = 0;
        StartCoroutine(ScoreCounter());
    }

    IEnumerator ScoreCounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            score++;
            scoreText.text = score.ToString();
        }
    }
}
