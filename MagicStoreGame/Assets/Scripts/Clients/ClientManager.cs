using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public List<GameObject> MicroWaveItems;
    public List<GameObject> PotionItems;
    public List<GameObject> SubwitchItems;
    
    [SerializeField] private ScalePoundMachine _PoundMachine;
    [SerializeField] private float _timeToNextClient;
    [SerializeField] private int _maxClientQuantity;
    [SerializeField] private GameObject _forgeClientObject;
    [SerializeField] private GameObject _sellClientObject;
    [SerializeField] private Transform _receptionPosition;
    [SerializeField] private Transform[] _storePositions;
    [SerializeField] private Transform _initialPosition;
    [SerializeField] private Transform _positionToPlaceItem;

    private float _clientTimeCount;
    private List<GameObject> _activeClientes = new List<GameObject>();
    public Vector3 _newPositionToGo;
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
                _activeClientes.Add(temp);
                tempForge.GenerateDictionary();
                Debug.Log(_newPositionToGo);
                tempForge.LocalToWalk(_storePositions[_activeClientes.Count - 1].position);
                randIndexClient = Random.Range(0, MicroWaveItems.Count);
                tempForge.SetItemOrder(MicroWaveItems[randIndexClient]);
                _PoundMachine._itensAwaitingForSell.Add(temp);
            }
        }
        else
        {
            GameObject temp = Instantiate(_sellClientObject, _initialPosition.position, Quaternion.identity);

            if(temp.TryGetComponent(out SellClient tempSell))
            {
                _activeClientes.Add(temp);
                Debug.Log(_newPositionToGo);
                tempSell.LocalToWalk(_storePositions[_activeClientes.Count - 1].position);
                tempSell.PositionToPlaceItem = _positionToPlaceItem;
                randIndexClient = Random.Range(0, MicroWaveItems.Count);
                tempSell.SetItemOrder(MicroWaveItems[randIndexClient]);
                _PoundMachine._itensAwaitingForSell.Add(temp);
            }
        }

    }

    public void RemoveClient(GameObject client)
    {
        _activeClientes.Remove(client);
    }
}
