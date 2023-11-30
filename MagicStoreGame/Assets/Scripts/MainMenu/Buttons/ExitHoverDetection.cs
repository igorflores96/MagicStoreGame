using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ExitHoverDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    public float scaleFactor; 

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
        ExitGame(); 
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
