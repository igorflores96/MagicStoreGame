using UnityEngine;

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

        Debug.Log("Active candles in the candelabro: " + activeCandles);
    }
}
