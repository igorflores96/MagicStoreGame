using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Label : MonoBehaviour
{
    [SerializeField] private string _labelName;


    public string LabelName
    {
        get {return _labelName;}
    }
}
