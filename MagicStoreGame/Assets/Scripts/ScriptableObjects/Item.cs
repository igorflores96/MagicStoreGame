using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items to make/Item", order = 1)]
public class Item : ScriptableObject
{
    public string ItemName;
    public GameObject ItemPrefab;
    public int ItemValue;
    public ScaleObject ScaleObjectPrefab;
}
