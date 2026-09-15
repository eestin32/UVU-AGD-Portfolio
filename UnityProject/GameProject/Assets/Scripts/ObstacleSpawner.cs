using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Transform camera;
    [SerializeField] private int spawnRange = 3;
    void OnEnable()
    {
        camera = Camera.main.transform;
        StartCoroutine(SpawnObstacles());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameObject spawnedObject = Instantiate(prefab, new Vector3(camera.position.x + 20f, camera.position.y + Random.Range(-spawnRange, spawnRange), 0), Quaternion.identity);
        }
    }
}
