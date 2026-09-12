using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed = 5f;
	public float EasingFactor = 5f;
    private Vector3 ScreenPosition;
    private float CameraDistance;
    private Vector2 MousePosition;
    private void Update()
	{
        CameraDistance = transform.position.z - Camera.main.transform.position.z;
        MousePosition = Mouse.current.position.ReadValue();
        ScreenPosition = new Vector3(MousePosition.x, MousePosition.y, CameraDistance);
        float newX = transform.position.x + speed * Time.deltaTime;
        // Convert easing factor to a per-frame interpolation value that is framerate-independent.
        // EasingFactor now represents a speed (higher = faster), applied with an exponential decay so
        // the smoothing behaves the same regardless of Time.deltaTime.
        float t = 1f - Mathf.Exp(-EasingFactor * Time.deltaTime);
        float newY = Mathf.Lerp(transform.position.y, Camera.main.ScreenToWorldPoint(ScreenPosition).y, t);
        Debug.Log(newY);
        transform.position = new Vector3(newX, newY, 0);
	}
}
