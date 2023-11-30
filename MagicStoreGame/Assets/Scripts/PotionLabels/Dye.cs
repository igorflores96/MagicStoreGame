using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dye : MonoBehaviour
{
    [SerializeField] Color _dyeColor;


    public Color DyeColor
    {
        get {return _dyeColor;}
    }
}
