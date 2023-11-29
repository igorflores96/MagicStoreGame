using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Potion : MonoBehaviour, IItem
{
    [SerializeField] private float _potionMaxMl;
    [SerializeField] private string _itemName;
    [SerializeField] private Transform _labelPosition;
    [SerializeField] private Transform _littleLabelPosition;

    [SerializeField] private GameObject _stopper;

    private Label _currentLabel;
    private Color _potionColor;
    private int _colorQuantity;
    private int _itemValue;
    private EnchantmentType _currentElement;
    private float _currentMl;
    private int _velocityWater;
    private bool _isPouring;
    private bool _stickLabel;
    private bool _dyePotion;

    void OnEnable()
    {
        _colorQuantity = 0;
        _stickLabel = false;
        _dyePotion = false;
       _currentMl = 0.0f; 
       _stopper.SetActive(false);
    }

    void Update()
    {
        if(_isPouring)
        {
            if(_currentMl < _potionMaxMl)
            {
                _currentMl += 1.0f * _velocityWater * Time.deltaTime;
                Debug.Log(_currentMl);
            }
            else
            {
                _currentMl = _potionMaxMl;
            }
        }
    }

    public void StartingPouringWater(bool status, int velocity)
    {
        _velocityWater = velocity;
        _isPouring = status;
    }

    public void StickTheLabel(Transform labelToStick, Label label)
    {
        if(!_stickLabel)
        {
            Destroy(labelToStick.GetComponent<Collider>());
            Destroy(labelToStick.GetComponent<Rigidbody>());
            labelToStick.position = _labelPosition.position;
            labelToStick.parent = this.transform;
            _itemName = label.LabelName;
            _stickLabel = true;
        }
    }

    public void ChangeLabelName(Transform littleLabelToStick, LittleLabel label)
    {
        if(_stickLabel)
        {

            Destroy(littleLabelToStick.GetComponent<Collider>());
            Destroy(littleLabelToStick.GetComponent<Rigidbody>());
            littleLabelToStick.position = _littleLabelPosition.position;
            littleLabelToStick.parent = this.transform;
            switch (_itemName)
            {
                case "Potion Of Wisdom": _itemName = "Potion Of " + label.LittleLabelName;
                break;
                case "Potion Of Eternal Youth": _itemName = "Potion Of Eternal " + label.LittleLabelName;
                break;
                case "Harmony Elixir": _itemName = label.LittleLabelName + " Elixir";
                break;
                case "Elixir Of Mind Control": _itemName = "Elixir Of " + label.LittleLabelName + " Control";
                break;
            }
        }
    }

    public void DyePotion(Color color)
    {
        if(!_dyePotion || _potionColor == color)
        {
            _potionColor = color;
            _colorQuantity++;
            _dyePotion = true;
            Debug.Log(_colorQuantity);
        }
    }

    public int IncreaseValueItem(int multiplier)
    {
        _itemValue *= multiplier;

        return _itemValue;
    }

    public bool IsPouring
    {
        get {return _isPouring;}
        set {_isPouring = value;}
    }

    public int ValueItem
    {
        get {return _itemValue;}
        set { _itemValue = value;} 
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

    public int PotionColorQuantity
    {
        get {return _colorQuantity;}
    }
}
