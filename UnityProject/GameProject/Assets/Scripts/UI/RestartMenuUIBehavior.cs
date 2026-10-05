using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RestartMenuUIBehavior : MonoBehaviour
{
    [SerializeField] private GameStateChannel gameStateChannel;
    private TextMeshProUGUI text;
    private Button button;
    private Image image;
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        text.enabled = false;
        button.enabled = false;
        image.enabled = false;
    }

    private void OnEnable()
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnRestart += OnRestart;
        gameStateChannel.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnRestart -= OnRestart;
        gameStateChannel.OnDeath -= OnDeath;
    }
    private void OnRestart()
    {
        text.enabled = false;
        button.enabled = false;
        image.enabled = false;
    }
    private void OnDeath()
    {
        text.enabled = true;
        button.enabled = true;
        image.enabled = true;
    }
}
