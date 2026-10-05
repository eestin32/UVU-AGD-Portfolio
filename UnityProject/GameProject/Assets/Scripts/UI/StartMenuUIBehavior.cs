using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartMenuUIBehavior : MonoBehaviour
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
        text.enabled = true;
        button.enabled = true;
        image.enabled = true;
    }

    private void OnEnable()
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnRestart += OnRestart;
        gameStateChannel.OnStart += OnStart;
    }

    private void OnDisable()
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnRestart -= OnRestart;
        gameStateChannel.OnStart -= OnStart;
    }
    private void OnStart()
    {
        text.enabled = false;
        button.enabled = false;
        image.enabled = false;
    }
    private void OnRestart()
    {
        text.enabled = true;
        button.enabled = true;
        image.enabled = true;
    }
}
