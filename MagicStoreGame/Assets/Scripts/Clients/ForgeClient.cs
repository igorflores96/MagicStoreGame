using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeClient : ClientBase
{   
    private Item _currentItem;
    private string _sentenceToOrder;
    public override void SetItemOrder(Item item)
    {
        _currentItem = item;
    }

    public override string SetDialogToSay()
    {
        _sentenceToOrder = DialogStorage.GetForgeInitialSentence(_currentItem.NameItem);
        CurrentSentence = _sentenceToOrder;
        return CurrentSentence;
    }

    public void GenerateDictionary()
    {
        DialogStorage.CreateDictionary();
    }
}
