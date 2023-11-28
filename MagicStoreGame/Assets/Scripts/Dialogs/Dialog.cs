using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Dialog : MonoBehaviour
{
        [Header("Two lists to make a dictionary.")]    
        public List<string> NameItemsKeys;
        public List<string> DialogItemsValues;

        public Dictionary<string, string> ItemsDialogDicionary;

    public string GetForgeFinalSentence(string itemQuality)
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

    public string GetForgeInitialSentence(string itemName)
    {
        string sentence;
        

        if(ItemsDialogDicionary.TryGetValue(itemName, out sentence))
            return sentence;
        else
            return "I forgot what I wanted.";           

    }

    public string GetSellInitialSentence()
    {
        return "Hi! Can you evaluate this item for me? I want to know how many scales I can get from it.";
    }

    public string GetSellFinalSentence()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0: return "What? Seriously, this item was fake? It was a family heirloom :( Well, thank you very much for your help.";
            case 1: return "No way! I've been keeping this item for years, and I needed some scales at the moment. :/";
            case 2: return "Really? But I was sure this item was genuine! Well, I'll take it to another store for evaluation; something must be wrong.";
            default: return "Well, ok.";
        }


    }

    public void CreateDictionary()
    {
        ItemsDialogDicionary = CreateDictionary(NameItemsKeys, DialogItemsValues);
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
