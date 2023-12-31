using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IItem, IScalable
{
    
    [SerializeField ] private bool _isOriginal;
    [SerializeField] private bool _itemScalable;
    [SerializeField] private float _maxSizeUp;
    [SerializeField] private float _maxSizeDown;
    [SerializeField] private float _scalePerFrame;
    [SerializeField] public string _itemName;
    [SerializeField] private EnchantmentType _currentElement;
    private int _priceItem;
    private bool _isScalingUp;
    private bool _isScalingDown;
    
    public int _itemValue = 0;
    public bool scalableFromRecipe;
    private void Awake() 
    {
        _isScalingUp = false;
        _isScalingDown = false;
    }

    void Update()
    {
        if (_isScalingUp && transform.localScale.x < _maxSizeUp)
        {
            Scale(_scalePerFrame);
        }

        if (_isScalingDown && transform.localScale.x > _maxSizeDown)
        {
            Scale(-_scalePerFrame);
        }
    }
    public void Scale(float scalingVal)
    {
        Vector3 novaEscala2 = transform.localScale + new Vector3(scalingVal, scalingVal, scalingVal);

        novaEscala2.x = Mathf.Clamp(novaEscala2.x, _maxSizeDown, _maxSizeUp);
        novaEscala2.y = Mathf.Clamp(novaEscala2.y, _maxSizeDown, _maxSizeUp);
        novaEscala2.z = Mathf.Clamp(novaEscala2.z, _maxSizeDown, _maxSizeUp);

        transform.localScale = novaEscala2;
    }

    public int ChangeValueItem(int value)
    {
        ValueItem = value;
        return ValueItem;
    }

    public int SetItemPrice(int indexMultiplier)
    {
        switch(indexMultiplier)
        {
            case 0: _priceItem = 1;
            break; 
            case 1: _priceItem = 3;
            break;
            case 2: _priceItem = 5;
            break;
        }

        return _priceItem;
    }

    public int ValueItem
    {
        get {return _itemValue;}
        set { _itemValue = value;} 
    }

    public bool IsScalingUp
    {
        get { return _isScalingUp;}
        set { _isScalingUp = value;} 
    }

    public bool IsScalingDown
    {
        get { return _isScalingDown;}
        set { _isScalingDown = value;} 
    }

    public string NameItem
    {
        get {return _itemName;}
        set { _itemName = value;}
    }

    public EnchantmentType EnchantmentItem
    {
        get {return _currentElement;}
        set {_currentElement = value;}
    }

    public bool IsOriginal
    {
        get {return _isOriginal;}
        set {_isOriginal = value;}
    }


    public int PriceItem
    {
        get {return _priceItem;}
    }
}
