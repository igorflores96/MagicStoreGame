using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    private float TimeOfDay;
    private int numberOfCandles = 5; // Este valor será atualizado pelo CandleHolder
    [SerializeField] private float secondsPerCandle = 60f; // Segundos por vela

    private void Start()
    {
        // Encontra o CandleHolder na cena
        CandleHolder candleHolder = FindObjectOfType<CandleHolder>();

        if (candleHolder != null)
        {
            // Obtém o número de velas do CandleHolder
            numberOfCandles = candleHolder.activeCandles;
        }
    }

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            float elapsedTime = Time.time; // Tempo decorrido desde o início do jogo

            float totalSecondsForDay = numberOfCandles * secondsPerCandle; // Total de segundos para o dia
            TimeOfDay = Mathf.Lerp(6f, 19f, elapsedTime / totalSecondsForDay); // Calcula o tempo do dia baseado no tempo decorrido e na duração total do dia
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
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
