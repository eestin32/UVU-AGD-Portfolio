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
    [SerializeField] private float pitchFactor = 1f;
    [SerializeField] private float volumeFactor = 130f;
    [SerializeField] private float baseVolume = 0.4f;
    private Camera camera;
    public float EasingFactor = 5f;
    public float audioEasingFactor = 2f;
    private float TargetY;
    private float CameraDistance;
    private Vector2 MousePosition;
    private Renderer renderer;
    private BoxCollider collider;
    private float minY, maxY;
    private Vector2 velocity;
    [SerializeField] private AudioSource windAudioSource, hitAudioSource;
    [SerializeField] private AudioClip windSound, hitSound;
    private Vector3 defaultPosition;
    private void Awake()
    {
        camera = Camera.main;
        renderer = GetComponent<Renderer>();
        collider = GetComponent<BoxCollider>();
        windAudioSource.clip = windSound;

        maxY = transform.position.y + maxDeviation; 
        minY = transform.position.y - maxDeviation;

        defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    IEnumerator HandleMovement()
    {
        float tm;
        float ta;
        windAudioSource.pitch = 1f;
        windAudioSource.volume = 0f;
        while (true)
        {
            MousePosition = Mouse.current.position.ReadValue();
            TargetY = camera.ScreenToWorldPoint(new Vector3(MousePosition.x, MousePosition.y, CameraDistance)).y;
            TargetY = Mathf.Clamp(TargetY, minY, maxY);
            tm = 1f - Mathf.Exp(-EasingFactor * Time.deltaTime);
            float newX = transform.position.x + speed * Time.deltaTime;
            float newY = Mathf.Lerp(transform.position.y, TargetY, tm);
            velocity = new Vector2((newX - transform.position.x) / Time.deltaTime, (newY - transform.position.y) / Time.deltaTime);
            transform.position = new Vector3(newX, newY, 0);
            transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, velocity.y, 0));

            // Change pitch and volume of wind sound effect based on velocity
            ta = 1f - Mathf.Exp(-audioEasingFactor * Time.deltaTime);
            windAudioSource.pitch = Mathf.Clamp(Mathf.Lerp(windAudioSource.pitch, 0.6f + Mathf.Abs(velocity.y) * pitchFactor / 100f, ta), 0f, 2.5f);
            windAudioSource.volume = Mathf.Clamp(Mathf.Lerp(windAudioSource.volume, baseVolume + Mathf.Abs(velocity.y) * volumeFactor / 100f, ta), 0f, .15f);

            yield return null; // Wait for the next frame
        }
    }
    IEnumerator DeathAnimation()
    {
        float timeElapsed = 0;
        float timeEnd = 2f;
        while (timeElapsed < timeEnd)
        {
            transform.position += new Vector3(-1f * Time.deltaTime, -0.5f * Mathf.Pow(timeElapsed, 2f), 0);
            transform.Rotate(new Vector3(-180f * Time.deltaTime, 0, 0));
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        renderer.enabled = false;
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

    private void OnDeath() // Stop movement
    {
        StopAllCoroutines();
        StartCoroutine(DeathAnimation());
        windAudioSource.Stop();
        hitAudioSource.PlayOneShot(hitSound);
        collider.enabled = false;
    }

    private void OnStart() // Start movement and ensure player is visible (fallback)
    {
        CameraDistance = transform.position.z - camera.transform.position.z;
        windAudioSource.Play();
        renderer.enabled = true;
        collider.enabled = true;
        StartCoroutine(HandleMovement());
    }

    private void OnRestart() // Reset player position and show player
    {
        StopAllCoroutines();
        transform.position = defaultPosition;
        transform.rotation = Quaternion.identity;
        renderer.enabled = true;
        collider.enabled = true;
    }
}
