using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameEnvironments gameEnvironments;
    private Transform camera;
    [SerializeField] private int spawnRange = 3;
    void OnEnable()
    {
        camera = Camera.main.transform;
        StartCoroutine(SpawnObstacles());
    }
    private void OnDisable()
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
