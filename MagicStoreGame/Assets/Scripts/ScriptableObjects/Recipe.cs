using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Items to make/Recipe", order = 1)]
public class Recipe : ScriptableObject
{   
    public string RecipeName;
    public GameObject RecipeItemPrefab;
    public List<Item> RecipeItems;
    public EnchantmentType RecipeEnchantment;

}

