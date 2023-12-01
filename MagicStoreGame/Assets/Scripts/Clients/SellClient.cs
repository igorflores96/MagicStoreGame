using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellClient : ClientBase
{
    [SerializeField] private TextDialog _textDialog;
    private Item _itemToOrder;
    private Potion _potionToOrder;
    private GameObject _currentItemOrder;
    private string _sentenceToOrder;
    private Transform _recepetionPosition;
    private Transform _positionToPlaceItem;
    private bool _isWalking = false;
    private bool _spawnItem = false;


    private void Update() 
    {
        if(_isWalking)
        {
            float step = 1.0f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _recepetionPosition.position, step);

            if (transform.position == _recepetionPosition.position)
            {
                _isWalking = false;
            }

        }
            
    }

    public override void SetItemOrder(GameObject item)
    {
        _currentItemOrder = item;
        CurrentItemOrder = _currentItemOrder;
    }


    public override void LocalToWalk(Vector3 position)
    {
        _recepetionPosition.position = position;
        _isWalking = true;
    }

    public override void SetDialogToSay()
    {
        _sentenceToOrder = DialogStorage.GetSellInitialSentence();
        CurrentSentence = _sentenceToOrder;
        _textDialog.Sentence = CurrentSentence;
        _textDialog.ShowSentence();
        if(!_spawnItem)
        {
            Instantiate(_currentItemOrder, _positionToPlaceItem.position, Quaternion.identity);
            _spawnItem = true;
        }
    } 

    public Transform PositionToPlaceItem
    {
        get {return _positionToPlaceItem;}
        set {_positionToPlaceItem = value;}
    } 
}
