using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GeneralObstacleBehavior : MonoBehaviour
{
    private Vector2 velocity;
    [SerializeField] private float speed = 5f;
    // Awake() is called once before Start() immediately at the start of an object's lifetime 
    void Awake()
    {
        float randomScale = Random.Range(0.5f, 2f);
        float randomAngle = Random.Range(0f, 360f);
        float randomSpeed = Random.Range(0f, 1f);
        velocity = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * randomSpeed;
        transform.localScale *= randomScale;
        StartCoroutine(WaitandKill());
    }
    void Update()
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0f) * Time.deltaTime * speed;
    }

    // Update is called once per frame
    IEnumerator WaitandKill()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
