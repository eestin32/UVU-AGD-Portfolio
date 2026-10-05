using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameStateChannel gameStateChannel;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxDeviation = 5f;
    private Camera camera;
    public float EasingFactor = 5f;
    private float TargetY;
    private float CameraDistance;
    private Vector2 MousePosition;
    private Renderer renderer;
    private float maxY;
    private float minY;
    private Vector3 defaultPosition;
    private void Awake()
    {
        camera = Camera.main;
        renderer = GetComponent<Renderer>();

        maxY = transform.position.y + maxDeviation; 
        minY = transform.position.y - maxDeviation;

        defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    IEnumerator HandleMovement()
    {
        while (true)
        {
            MousePosition = Mouse.current.position.ReadValue();
            TargetY = camera.ScreenToWorldPoint(new Vector3(MousePosition.x, MousePosition.y, CameraDistance)).y;
            TargetY = Mathf.Clamp(TargetY, minY, maxY);
            float newX = transform.position.x + speed * Time.deltaTime;
            float t = 1f - Mathf.Exp(-EasingFactor * Time.deltaTime);
            float newY = Mathf.Lerp(transform.position.y, TargetY, t);
            transform.position = new Vector3(newX, newY, 0);
            yield return null; // Wait for the next frame
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            gameStateChannel.RaiseDeath();
        }
    }
    private void OnEnable() // Subscribe to start, restart and death events from the game state channel
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnStart += OnStart;
        gameStateChannel.OnRestart += OnRestart;
        gameStateChannel.OnDeath += OnDeath;
    }

    private void OnDisable() // Unsubscribe from events when object is disabled
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnStart -= OnStart;
        gameStateChannel.OnRestart -= OnRestart;
        gameStateChannel.OnDeath -= OnDeath;
    }

    private void OnDeath() // Stop movement and hide player
    {
        StopAllCoroutines();
        renderer.enabled = false;
    }

    private void OnStart() // Start movement and ensure player is visible (fallback)
    {
        CameraDistance = transform.position.z - camera.transform.position.z;
        renderer.enabled = true;
        StartCoroutine(HandleMovement());
    }

    private void OnRestart() // Reset player position and show player
    {
        transform.position = defaultPosition;
        renderer.enabled = true;
    }
}
