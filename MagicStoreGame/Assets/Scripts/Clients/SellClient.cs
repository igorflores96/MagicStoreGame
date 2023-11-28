using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellClient : ClientBase
{
    private Item _currentItem;
    private string _sentenceToOrder;

    public override void SetItemOrder(Item item)
    {
        _currentItem = item;
    }

    public override string SetDialogToSay()
    {
        _sentenceToOrder = DialogStorage.GetSellInitialSentence();
        CurrentSentence = _sentenceToOrder;
        return CurrentSentence;
    }   
}
