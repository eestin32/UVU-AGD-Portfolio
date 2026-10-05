using UnityEngine;

[CreateAssetMenu(fileName = "GameStateChannel", menuName = "Scriptable Objects/GameStateChannel")]
public class GameStateChannel : ScriptableObject
{
    public delegate void StartCallback();
    public StartCallback OnStart;
    public delegate void RestartCallback();
    public RestartCallback OnRestart;
    public delegate void DeathCallback();
    public DeathCallback OnDeath;

    public void RaiseStart()
    {
                OnStart?.Invoke();
    }

    public void RaiseRestart()
    {
                OnRestart?.Invoke();
    }

    public void RaiseDeath()
    {
                OnDeath?.Invoke();
    }
}
