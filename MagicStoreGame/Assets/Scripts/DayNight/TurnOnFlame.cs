using UnityEngine;

public class TurnOnFlame : MonoBehaviour
{
    void Start()
    {
        // Check if this object is active
        if (gameObject.activeSelf)
        {
            // Activate the first child object if available
            if (transform.childCount > 0)
            {
                Transform firstChild = transform.GetChild(0);
                firstChild.gameObject.SetActive(true);
            }
        }
    }
}
