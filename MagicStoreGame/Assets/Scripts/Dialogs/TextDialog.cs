using System.Collections;
using TMPro;
using UnityEngine;

public class TextDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private float typingSpeed;
    [SerializeField] private GameObject dialogCanvas;
    private string _sentenceToDisplay;
    private Coroutine typingCoroutine;

    private void OnEnable() {
        dialogCanvas.SetActive(false);
    }
    IEnumerator Type()
    {
        foreach ( char letter in _sentenceToDisplay.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void ShowSentence()
    {
        if(typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogCanvas.SetActive(true);
        if (_sentenceToDisplay != null)
        {
            textDisplay.text = "";
            
            typingCoroutine = StartCoroutine(Type());
            _sentenceToDisplay = null;
        }
    }

    public string Sentence
    {
        get { return _sentenceToDisplay;}
        set {_sentenceToDisplay = value;}
    }

}
