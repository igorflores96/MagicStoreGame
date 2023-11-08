using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items to make/Item", order = 1)]
public class Item : ScriptableObject
{
    string ItemName;
    GameObject ItemPrefab;
    int ItemValue;
    float ItemXMinScale, ItemYMinScale, ItemZMinScale;
    float ItemXMaxScale, ItemYMaxScale, ItemZMaxScale;
}
