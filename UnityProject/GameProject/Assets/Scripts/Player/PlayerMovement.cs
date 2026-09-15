using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed = 5f;
	public float EasingFactor = 5f;
    private float TargetY;
    private float CameraDistance;
    private Vector2 MousePosition;
    private Renderer renderer;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    private void Update()
	{
        CameraDistance = transform.position.z - Camera.main.transform.position.z;
        MousePosition = Mouse.current.position.ReadValue();
        TargetY = Camera.main.ScreenToWorldPoint(new Vector3(MousePosition.x, MousePosition.y, CameraDistance)).y;
        TargetY = Mathf.Clamp(TargetY, 45f, 55f);
        float newX = transform.position.x + speed * Time.deltaTime;
        // Convert easing factor to a per-frame interpolation value that is framerate-independent.
        // EasingFactor now represents a speed (higher = faster), applied with an exponential decay so
        // the smoothing behaves the same regardless of Time.deltaTime.
        float t = 1f - Mathf.Exp(-EasingFactor * Time.deltaTime);
        float newY = Mathf.Lerp(transform.position.y, TargetY, t);
        transform.position = new Vector3(newX, newY, 0);
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            renderer.enabled = false;
            UnityEngine.Debug.Log("Player hit an obstacle!");
        }
    }
}
