using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellClient : ClientBase
{
    [SerializeField] private TextDialog _textDialog;
    private Item _currentItem;
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

    public override void SetItemOrder(Item item)
    {
        _currentItem = item;
    }


    public override void LocalToWalk(Transform position)
    {
        _recepetionPosition = position;
        isWalking = true;
    }

    public override void SetDialogToSay()
    {
        _sentenceToOrder = DialogStorage.GetSellInitialSentence();
        CurrentSentence = _sentenceToOrder;
        _textDialog.Sentence = CurrentSentence;
        _textDialog.ShowSentence();
    }   
}
