using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public float secondsPerCandle; // Time in seconds to reduce scale per candle
    private int activeCandles; // This value will be updated by the CandleHolder
    public Light candleLight; // Reference to the light

    private float originalIntensity; // Original intensity of the light
    private float totalTime; // Total time to decrease the light intensity
    private bool reduceIntensity = false; // Control for intensity reduction
    private float intensityReductionStartTime; // Time when intensity reduction starts

    // Start is called before the first frame update
    void Start()
    {
        UpdateCandles();
        originalIntensity = candleLight.intensity;

        // Calculate total time based on the number of active candles
        totalTime = secondsPerCandle * activeCandles;

        // Start reducing intensity after 1 second
        Invoke("StartIntensityReduction", 1f);
    }

    void Update()
    {
        if (reduceIntensity)
        {
            ReduceIntensity();
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

    private void StartIntensityReduction()
    {
        reduceIntensity = true;
        intensityReductionStartTime = Time.time;
    }

    private void ReduceIntensity()
    {
        // Reduce the light intensity based on time
        float progress = (Time.time - intensityReductionStartTime) / totalTime;
        candleLight.intensity = Mathf.Lerp(originalIntensity, 0.5f, progress);

        // Check if the intensity has reached the minimum (0.5f)
        if (candleLight.intensity <= 0.5f)
        {
            candleLight.intensity = 0.5f; // Ensure intensity is 0.5
            reduceIntensity = false;
            Debug.Log("Light intensity reduced!");
        }
    }
}
