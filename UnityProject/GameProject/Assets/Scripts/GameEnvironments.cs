using UnityEngine;

public class GameEnvironments : MonoBehaviour
{
    [SerializeField] public EnvironmentSettings[] list;
    public EnvironmentSettings current;

    void Awake()
    {
        current = list[0];
    }
}
