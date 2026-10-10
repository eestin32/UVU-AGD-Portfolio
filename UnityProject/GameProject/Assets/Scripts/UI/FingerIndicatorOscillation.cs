using UnityEngine;
using UnityEngine.UI;

public class FingerIndicatorOscillation : MonoBehaviour
{
    private Vector2 startPosition;
    [SerializeField] private float oscillationSpeed = 1f;
    [SerializeField] private float oscillationMagnitude = 1f;
    [SerializeField] private GameStateChannel gameStateChannel;
    private Image image;
    void Start()
    {
        startPosition = new Vector2(transform.position.x, transform.position.y);
        image = GetComponent<Image>();
    }
    void Update()
    {
        transform.position = new Vector2(startPosition.x, startPosition.y + Mathf.Sin(Time.time * oscillationSpeed) * oscillationMagnitude);
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
    void OnStart()
    {
        image.enabled = false;
    }
    void OnRestart()
    {
        image.enabled = true;
    }
}
