using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public int ValueItem {get;set;}
    public int IncreaseValueItem(int multiplier);
}
