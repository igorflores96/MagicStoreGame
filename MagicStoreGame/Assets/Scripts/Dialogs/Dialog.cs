using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
        [Header("Two lists to make a dictionary.")]    
        public List<string> NameItemsKeys;
        public List<string> DialogItemsValues;

        public Dictionary<string, string> ItemsDialogDicionary;

    private void OnEnable()
    {
        ItemsDialogDicionary = CreateDictionary(NameItemsKeys, DialogItemsValues);
    }

    public string GetFinalSentence(string itemQuality)
    {
        string dialog;
        
        switch(itemQuality)
        {
            case "Common": dialog = "Great! I've seen better, but thank you anyway. Here's your scales!";
            break;
            case "Perfect": dialog = "This is perfect! Your store is amazing! Thank you so much. Here's your scales!";
            break;
            case "Bad": dialog = "I really hope this works. Thanks. Here are your scales.";
            break;
            default: dialog = "What is this? This is not what I asked for.";
            break;
        }

        return dialog;
    }

    public string GetInitialSentence(string itemName)
    {
        string sentence;

        if(ItemsDialogDicionary.TryGetValue(itemName, out sentence))
            return sentence;
        else
            return "I forgot what I wanted. ";

    }

    private Dictionary<string, string> CreateDictionary(List<string> keys, List<string> values)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        if (keys.Count == values.Count)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
            }
        }
        else
        {
            Debug.LogError("Please, create two lists with the same size.");
        }

        return dictionary;
    }
}
