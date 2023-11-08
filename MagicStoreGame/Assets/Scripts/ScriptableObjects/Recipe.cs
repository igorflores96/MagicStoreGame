using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Items to make/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    string RecipeName;
    List<Item> RecipeItems;
    Item RecipeItemPrefab;
}
