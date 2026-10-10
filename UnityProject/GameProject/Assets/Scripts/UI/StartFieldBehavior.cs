using UnityEngine;
using UnityEngine.UI;

public class StartFieldBehavior : MonoBehaviour
{
    [SerializeField] private GameStateChannel gameStateChannel;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
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
        image.enabled = false;
    }

    private void OnRestart()
    {
        image.enabled = true;
    }
}
