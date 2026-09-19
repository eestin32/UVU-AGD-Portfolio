using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentSettings", menuName = "Game/EnvironmentSettings", order = 1)]
public class EnvironmentSettings : ScriptableObject
{
    public GameObject[] obstaclePrefabs;
    public float obstacleDensity = 1.0f;
    public float obstacleSpeed = 1.0f;
    public Color skyColor1 = new Color(0.5f, 0.5f, 0.5f, 1f);
    public Color skyColor2 = new Color(0.5f, 0.5f, 0.5f, 1f);
}
