using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    private float TimeOfDay;
    private int activeCandles; // Este valor será atualizado pelo CandleHolder
    [SerializeField] private float secondsPerCandle; // Segundos por vela

    private void Start()
    {
        UpdateCandles();
    }

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            float elapsedTime = Time.time; // Tempo decorrido desde o início do jogo

            UpdateTimeOfDay(elapsedTime);
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateTimeOfDay(float elapsedTime)
    {
        float totalSecondsForDay = activeCandles * secondsPerCandle; // Total de segundos para o dia
        TimeOfDay = Mathf.Lerp(6f, 19f, elapsedTime / totalSecondsForDay); // Calcula o tempo do dia baseado no tempo decorrido e na duração total do dia
    }

    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 350f, 0));
        }
    }

    private void UpdateCandles()
    {
        activeCandles = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.activeSelf)
            {
                activeCandles++;
            }
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}

public class CandleHolder : MonoBehaviour
{
    public int activeCandles { get; private set; } = 0;

    private void Start()
    {
        CountActiveCandles();
    }

    private void CountActiveCandles()
    {
        activeCandles = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.activeSelf)
            {
                activeCandles++;
            }
        }
    }
}
