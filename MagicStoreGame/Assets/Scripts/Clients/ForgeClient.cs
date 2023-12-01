using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ForgeClient : ClientBase
{   
    
    [SerializeField] private TextDialog _textDialog;
    private Item _itemToOrder;
    private Potion _potionToOrder;
    private GameObject _currentItemOrder;
    private string _sentenceToOrder;
    private Transform _recepetionPosition;
    private bool isWalking = false;




    private void Update() 
    {
        if(isWalking)
        {
            float step = 1.0f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _recepetionPosition.position, step);

            if (transform.position == _recepetionPosition.position)
            {
                isWalking = false;
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
        isWalking = true;
    }

    public override void SetDialogToSay()
    {
        if(_currentItemOrder.TryGetComponent(out IItem item))
        {
            _sentenceToOrder = DialogStorage.GetForgeInitialSentence(item.NameItem);
        }
        
        CurrentSentence = _sentenceToOrder;

        _textDialog.Sentence = CurrentSentence;
        _textDialog.ShowSentence();

    }

    public void GenerateDictionary()
    {
        DialogStorage.CreateDictionary();
    }
}
