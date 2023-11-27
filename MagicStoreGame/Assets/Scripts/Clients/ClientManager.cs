using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public List<Item> MicroWaveItems;
    [SerializeField] private ForgeClient _forgeClient;
    private ClientBase _currentClientToLaunch;

    private void Awake() 
    {
        PrepareClient();
    }

    
    private void PrepareClient()
    {
        int randIndex = Random.Range(0, MicroWaveItems.Count);
        Item temp = MicroWaveItems[randIndex];
        _forgeClient.SetItemOrder(temp);
        Debug.Log(_forgeClient.SetDialogToSay());
    }
}
