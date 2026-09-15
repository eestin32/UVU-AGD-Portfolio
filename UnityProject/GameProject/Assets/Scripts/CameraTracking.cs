using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField] private Transform target; // The target object to follow
    [SerializeField] private int offset; // The offset from the target's position

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x + offset, transform.position.y, transform.position.z); // Follow the target's x position with offset
    }
}
