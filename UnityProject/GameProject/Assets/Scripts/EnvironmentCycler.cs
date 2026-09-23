using System.Collections;
using UnityEngine;

public class EnvironmentCycler : MonoBehaviour
{
    [SerializeField] private GameEnvironments gameEnvironments;
    void Start()
    {
        StartCoroutine(EnvironmentCycle());
        setVariablesInstant();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator SetVariables()
    {
        float timeElapsed = 0f;
        float duration = 3f;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            Shader.SetGlobalColor("_TopColor", Color.Lerp(Shader.GetGlobalColor("_TopColor"), gameEnvironments.current.skyColor1, t));
            Shader.SetGlobalColor("_BottomColor", Color.Lerp(Shader.GetGlobalColor("_BottomColor"), gameEnvironments.current.skyColor2, t));
            RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, gameEnvironments.current.skyColor1, t);
            RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, gameEnvironments.current.skyColor2, t);
            RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, gameEnvironments.current.terrainColor, t);
            yield return null;
        }
        setVariablesInstant();
    }
    private void setVariablesInstant()
    {
        Shader.SetGlobalColor("_TopColor", gameEnvironments.current.skyColor1);
        Shader.SetGlobalColor("_BottomColor", gameEnvironments.current.skyColor2);
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