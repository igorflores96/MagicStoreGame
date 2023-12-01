using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public int ValueItem {get;set;}
    public string NameItem {get;set;}
    public EnchantmentType EnchantmentItem {get;set;}

    public int ChangeValueItem(int value);
}
