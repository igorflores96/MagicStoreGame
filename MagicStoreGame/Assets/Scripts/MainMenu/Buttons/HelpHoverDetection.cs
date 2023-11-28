using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class HelpHoverDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    public float scaleFactor;
    public float amountYToMove;
    public GameObject cameraControlerObject; // Assign the GameObject you want to move in the Inspector
    public GameObject eventSystemObject; // Assign the EventSystem GameObject in the Inspector

    void Start()
    {
        originalScale = transform.localScale; // Save the original scale of the object
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 increasedScale = originalScale + originalScale * scaleFactor; // Calculate the increased scale
        transform.localScale = increasedScale; // When the mouse is over, increase the scale
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale; // When the mouse exits, return to the original scale
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DisableEventSystem();
        MoveCameraControler();
    }

    void DisableEventSystem()
    {
        if (eventSystemObject != null)
        {
            eventSystemObject.SetActive(false); // Disable the EventSystem GameObject
        }
    }

    void EnableEventSystem()
    {
        if (eventSystemObject != null)
        {
            eventSystemObject.SetActive(true); // Enable the EventSystem GameObject
        }
    }

    void MoveCameraControler()
    {
        if (cameraControlerObject != null)
        {
            Vector3 targetPosition = cameraControlerObject.transform.position - new Vector3(0, amountYToMove, 0);
            StartCoroutine(MoveObject(cameraControlerObject.transform, targetPosition, 1f));
        }
    }

    IEnumerator MoveObject(Transform objectTransform, Vector3 targetPosition, float duration)
    {
        float timeElapsed = 0f;
        Vector3 startingPosition = objectTransform.position;

        while (timeElapsed < duration)
        {
            objectTransform.position = Vector3.Lerp(startingPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        objectTransform.position = targetPosition;
        EnableEventSystem(); // Re-enable the EventSystem GameObject after movement
    }
}
