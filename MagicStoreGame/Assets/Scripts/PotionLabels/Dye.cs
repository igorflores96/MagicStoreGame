using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dye : MonoBehaviour
{
    [SerializeField] private Color _dyeColor;
    [SerializeField] private string _colorName;


    public Color DyeColor
    {
        get {return _dyeColor;}
    }

    public string DyeName
    {
        get {return _colorName;}
    }
}
