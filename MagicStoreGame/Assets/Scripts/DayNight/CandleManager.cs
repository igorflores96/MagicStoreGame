using UnityEngine;

public class CandleManager : MonoBehaviour
{
    public float secondsPerCandle = 5f; // Time in seconds to reduce scale per candle
    private GameObject[] candles; // Array to store active candles
    private int currentCandleIndex; // Index of the current active candle
    private bool reduceScale = false; // Variable to control scale reduction
    private float targetScaleFactor = 0.05f; // Target scale factor (5% of original scale)

    private float scaleStartTime; // Time when scale reduction starts for a candle
    private Vector3[] originalScales; // Array to store original scales of candles

    void Start()
    {
        // Find all objects with the "CandleFlame" tag
        GameObject[] allCandles = GameObject.FindGameObjectsWithTag("CandleFlame");
        
        // Filter active candles
        int activeCandlesCount = 0;
        foreach (GameObject candle in allCandles)
        {
            if (candle.activeSelf)
            {
                activeCandlesCount++;
            }
        }

        // Create an array for active candles
        candles = new GameObject[activeCandlesCount];
        int index = 0;
        foreach (GameObject candle in allCandles)
        {
            if (candle.activeSelf)
            {
                candles[index] = candle;
                index++;
            }
        }

        originalScales = new Vector3[candles.Length];

        // Record original scales of active candles
        RecordOriginalScales();

        // Start reducing candles scale
        StartScaleReduction();
    }

    void Update()
    {
        if (reduceScale)
        {
            if (currentCandleIndex < candles.Length)
            {
                GameObject currentCandle = candles[currentCandleIndex];
                if (currentCandle.activeSelf)
                {
                    ReduceCandleScale(currentCandle, currentCandleIndex);
                }
                else
                {
                    // Move to the next candle if the current one is inactive
                    currentCandleIndex++;
                }
            }
            else
            {
                // All candles have been extinguished
                reduceScale = false;
                Debug.Log("All candles extinguished!");
            }
        }
    }

    void RecordOriginalScales()
    {
        for (int i = 0; i < candles.Length; i++)
        {
            originalScales[i] = candles[i].transform.localScale;
        }
        // Start with the first candle index
        currentCandleIndex = 0;
    }

    void ReduceCandleScale(GameObject candleObject, int index)
    {
        Transform candleTransform = candleObject.transform;

        float progress = (Time.time - scaleStartTime) / secondsPerCandle;
        float currentScaleFactor = Mathf.Lerp(1f, targetScaleFactor, progress);

        Vector3 newScale = originalScales[index] * currentScaleFactor;
        candleTransform.localScale = newScale;

        // Check if the candle has reached the target scale
        if (currentScaleFactor <= targetScaleFactor)
        {
            // Deactivate the candle and move to the next one
            candleObject.SetActive(false);
            currentCandleIndex++;
            scaleStartTime = Time.time;
        }
    }

    void StartScaleReduction()
    {
        reduceScale = true;
        scaleStartTime = Time.time;
    }
}
