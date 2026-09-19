using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreCounterBehavior : MonoBehaviour
{
    public int score = 0;
    private TextMeshProUGUI scoreText;

    void OnEnable()
    {
        score = 0;
        scoreText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ScoreCounter());
    }

    IEnumerator ScoreCounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            score++;
            scoreText.text = score.ToString();
        }
    }
}
