using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public List<Item> MicroWaveItems;
    [SerializeField] private TextDialog _textDialog;
    [SerializeField] private ForgeClient _forgeClient;
    [SerializeField] private SellClient _sellClient;
    private ClientBase _currentClient;

    private void OnEnable() 
    {
        _forgeClient.GenerateDictionary();
        PrepareClient();
    }

    
    private void PrepareClient()
    {
        int randIndex = Random.Range(0, MicroWaveItems.Count);
        Item temp = MicroWaveItems[randIndex];
        randIndex = Random.Range(0, 2);
        
        if(randIndex == 0)
        {
            _currentClient = _forgeClient;
        }
        else
        {
            _currentClient = _sellClient;
        }

        _currentClient.SetItemOrder(temp);
        _textDialog.Sentence = _currentClient.SetDialogToSay();
        _textDialog.ShowSentence();
    }
}
