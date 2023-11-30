using UnityEngine;

public class FlamesManager : MonoBehaviour
{
    public float secondsPerCandle = 5f; // Time in seconds to reduce scale per candle
    private GameObject[] candles; // Array to store candles
    private int currentCandleIndex; // Index of the current active candle
    private bool reduceScale = false; // Variable to control scale reduction
    private float targetScaleFactor = 0.05f; // Target scale factor (5% of original scale)

    private float scaleStartTime; // Time when scale reduction starts for a candle
    private Vector3[] originalScales; // Array to store original scales of candles

    void Start()
    {
        // Find and store candles with the "CandleFlame" tag
        GameObject[] foundCandles = GameObject.FindGameObjectsWithTag("CandleFlame");
        // Sort candles based on their order in the hierarchy
        System.Array.Sort(foundCandles, (candle1, candle2) => candle1.transform.GetSiblingIndex().CompareTo(candle2.transform.GetSiblingIndex()));
        // Check and store only the active candles
        candles = System.Array.FindAll(foundCandles, candle => candle.activeSelf);

        originalScales = new Vector3[candles.Length];

        // Record original scales of active candles
        RecordOriginalScales();

        // Start reducing candles scale from the first active candle
        currentCandleIndex = 0; // Start from the first candle
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
            }
        }
    }

    void RecordOriginalScales()
    {
        for (int i = 0; i < candles.Length; i++)
        {
            originalScales[i] = candles[i].transform.localScale;
        }
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

            // If all candles have been extinguished, stop reducing scale
            if (currentCandleIndex >= candles.Length)
            {
                reduceScale = false;
            }
            else
            {
                scaleStartTime = Time.time; // Reset the scale start time for the next candle
            }
        }
    }

    void StartScaleReduction()
    {
        reduceScale = true;
        scaleStartTime = Time.time;
    }
}
