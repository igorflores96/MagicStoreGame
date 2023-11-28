using System.Collections;
using TMPro;
using UnityEngine;

public class TextDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private float typingSpeed;
    [SerializeField] private GameObject dialogCanvas;
    private string _sentenceToDisplay;

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
        if (_sentenceToDisplay != null)
        {
            textDisplay.text = "";
            
            StartCoroutine(Type());
            _sentenceToDisplay = null;
        }
    }

    public string Sentence
    {
        get { return _sentenceToDisplay;}
        set {_sentenceToDisplay = value;}
    }

}
