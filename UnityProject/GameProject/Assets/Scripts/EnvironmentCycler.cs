using System.Collections;
using UnityEngine;

public class EnvironmentCycler : MonoBehaviour
{
    [SerializeField] private GameStateChannel gameStateChannel;
    [SerializeField] private GameEnvironments gameEnvironments;
    
    void Start()
    {
        gameEnvironments.current = gameEnvironments.list[0];
        setVariablesInstant();
    }
    private void OnEnable()
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnStart += OnStart;
        gameStateChannel.OnDeath += OnDeath;
        gameStateChannel.OnRestart += OnRestart;
    }

    private void OnDisable()
    {
        if (gameStateChannel == null) return;
        gameStateChannel.OnStart -= OnStart;
        gameStateChannel.OnDeath -= OnDeath;
        gameStateChannel.OnRestart -= OnRestart;
    }
    private void OnStart()
    {
        StartCoroutine(EnvironmentCycle());
    }
    private void OnDeath()
    {
        StopAllCoroutines();
    }
    private void OnRestart()
    {
        gameEnvironments.current = gameEnvironments.list[0];
        setVariablesInstant();
    }
    IEnumerator SetVariables()
    {
        float timeElapsed = 0f;
        float duration = 3f;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            Shader.SetGlobalColor("_TopColor", Color.Lerp(Shader.GetGlobalColor("_TopColor"), gameEnvironments.current.skyColor1.linear, t));
            Shader.SetGlobalColor("_BottomColor", Color.Lerp(Shader.GetGlobalColor("_BottomColor"), gameEnvironments.current.skyColor2.linear, t));
            RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, gameEnvironments.current.skyColor1, t);
            RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, gameEnvironments.current.skyColor2, t);
            RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, gameEnvironments.current.terrainColor, t);
            yield return null;
        }
        setVariablesInstant();
    }
    private void setVariablesInstant()
    {
        Shader.SetGlobalColor("_TopColor", gameEnvironments.current.skyColor1.linear);
        Shader.SetGlobalColor("_BottomColor", gameEnvironments.current.skyColor2.linear);

        RenderSettings.ambientSkyColor = gameEnvironments.current.skyColor1;
        RenderSettings.ambientEquatorColor = gameEnvironments.current.skyColor2;
        RenderSettings.ambientGroundColor = gameEnvironments.current.terrainColor;
    }
    IEnumerator EnvironmentCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            int randomIndex = Random.Range(0, gameEnvironments.list.Length);
            gameEnvironments.current = gameEnvironments.list[randomIndex];
            StartCoroutine(SetVariables());
        }
    }
}