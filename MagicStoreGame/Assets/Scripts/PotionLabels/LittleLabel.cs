using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleLabel : MonoBehaviour
{
    [SerializeField] private string _littleLabelName;

    public string LittleLabelName
    {
        get {return _littleLabelName;}
    }
}
