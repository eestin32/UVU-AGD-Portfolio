using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameStateChannel gameStateChannel;
    [SerializeField] private GameEnvironments gameEnvironments;
    [SerializeField] private int spawnRange = 3;
    private Transform camera;

    void Start()
    {
        camera = Camera.main.transform;
    }
    private void OnEnable()
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnStart += OnStart;
        gameStateChannel.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnStart -= OnStart;
        gameStateChannel.OnDeath -= OnDeath;
    }
    private void OnStart()
    {
        GameObject[] existingObstacles = GameObject.FindGameObjectsWithTag("Obstacle"); for (int i = 0; i < existingObstacles.Length; i++) Destroy(existingObstacles[i]);
        StartCoroutine(SpawnObstacles());
    }
    private void OnDeath()
    {
        StopAllCoroutines();
    }
    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / gameEnvironments.current.obstacleDensity);
            int randomIndex = Random.Range(0, gameEnvironments.current.obstaclePrefabs.Length);
            GameObject newObject = Instantiate(gameEnvironments.current.obstaclePrefabs[randomIndex], new Vector3(camera.position.x + 20f, camera.position.y + Random.Range(-spawnRange, spawnRange), 0), Quaternion.identity);
            newObject.GetComponent<GeneralObstacleBehavior>().velocity.x -= gameEnvironments.current.obstacleSpeed;
        }
    }
}
