using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private EnvironmentSettings[] environmentList;
    private EnvironmentSettings currentEnvironment;
    private Transform camera;
    private int randomIndex;
    [SerializeField] private int spawnRange = 3;
    void OnEnable()
    {
        camera = Camera.main.transform;
        currentEnvironment = environmentList[0];
        StartCoroutine(SpawnObstacles());
        StartCoroutine(EnvironmentCycle());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f * currentEnvironment.obstacleDensity);
            randomIndex = Random.Range(0, currentEnvironment.obstaclePrefabs.Length);
            Instantiate(currentEnvironment.obstaclePrefabs[randomIndex], new Vector3(camera.position.x + 20f, camera.position.y + Random.Range(-spawnRange, spawnRange) * currentEnvironment.obstacleSpeed, 0), Quaternion.identity);
        }
    }

    IEnumerator EnvironmentCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            randomIndex = Random.Range(0, environmentList.Length);
            currentEnvironment = environmentList[randomIndex];
            Shader.SetGlobalColor("TopColor", currentEnvironment.skyColor1);
            Shader.SetGlobalColor("BottomColor", currentEnvironment.skyColor2);
        }
    }
}
