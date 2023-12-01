using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public List<Item> MicroWaveItems;
    public List<Item> PotionItems;
    public List<Item> SubwitchItems;
    
    [SerializeField] private float _timeToNextClient;
    [SerializeField] private int _maxClientQuantity;
    [SerializeField] private GameObject _forgeClientObject;
    [SerializeField] private GameObject _sellClientObject;
    [SerializeField] private Transform _receptionPosition;
    [SerializeField] private Transform[] _storePositions;
    [SerializeField] private Transform _initialPosition;

    private float _clientTimeCount;
    private List<ClientBase> _activeClientes = new List<ClientBase>();

    private void OnEnable() 
    {
        _clientTimeCount = _timeToNextClient;
    }

    private void Update() 
    {
        _clientTimeCount -= 1.0f * Time.deltaTime;

        if(_clientTimeCount <= 0.0f && _activeClientes.Count < _maxClientQuantity)
        {
            PrepareClient();
            _clientTimeCount = _timeToNextClient;
        }
    }

    
    private void PrepareClient()
    {
        int randIndexClient = Random.Range(0, 2);
        
        if(randIndexClient == 0)
        {
            GameObject temp = Instantiate(_forgeClientObject, _initialPosition.position, Quaternion.identity);

            if(temp.TryGetComponent(out ForgeClient tempForge))
            {
                tempForge.GenerateDictionary();
                _activeClientes.Add(tempForge);
                tempForge.LocalToWalk(_storePositions[_activeClientes.Count - 1]);

                randIndexClient = Random.Range(0, MicroWaveItems.Count);
                tempForge.SetItemOrder(MicroWaveItems[randIndexClient]);
            }
        }
        else
        {
            GameObject temp = Instantiate(_sellClientObject, _initialPosition.position, Quaternion.identity);

            if(temp.TryGetComponent(out SellClient tempSell))
            {
                _activeClientes.Add(tempSell);
                tempSell.LocalToWalk(_storePositions[_activeClientes.Count - 1]);
                randIndexClient = Random.Range(0, MicroWaveItems.Count);
                tempSell.SetItemOrder(MicroWaveItems[randIndexClient]);
            }
        }
       




    }
}
