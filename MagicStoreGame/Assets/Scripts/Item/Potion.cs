using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Potion : MonoBehaviour, IItem
{
    [SerializeField] private float _potionMaxMl;
    [SerializeField] private string _itemName;
    [SerializeField] private Transform _labelPosition;
    [SerializeField] private Transform _harmonyPositon;
    [SerializeField] private Transform _mindPosition;
    [SerializeField] private Transform _wisdomPosition;
    [SerializeField] private Transform _youthPosition;
    [SerializeField] private GameObject _wather;
    [SerializeField] private GameObject _dye;
    [SerializeField] private GameObject _oil;
    [SerializeField] private GameObject _stopper;
    [SerializeField] private Material _redOilMaterial;
    [SerializeField] private Material _blueOilMaterial;
    [SerializeField] private Material _yellowOilMaterial;
    private Color _potionColor;
    private int _colorQuantity;
    private int _itemValue;
    private EnchantmentType _currentElement;
    private float _currentMl;
    private float _velocityToPouring;
    private bool _isPouringWater;
    private bool _oilAdd;
    private bool _stickLabel;
    private bool _dyePotion;
    private string _colorName;
    private Coroutine startFireCoroutine;

    void OnEnable()
    {
        _colorQuantity = 0;
        _stickLabel = false;
        _dyePotion = false;
        _oilAdd = false;
       _currentMl = 0.0f; 
       _stopper.SetActive(false);
       _oil.SetActive(false);
    }

    void Update()
    {
        if(_isPouringWater)
        {
            if(_currentMl < _potionMaxMl)
            {
                _currentMl += 1.0f * _velocityToPouring * Time.deltaTime;
                float scale;
                
                if(_potionMaxMl == 15.0f)
                    scale = 0.018f;
                else
                    scale = 1.6f;

                float newScaleY = Mathf.Clamp(_currentMl / _potionMaxMl, 0.0f, 1.0f) * scale;
                _wather.SetActive(true);
                _wather.transform.localScale = new Vector3(_wather.transform.localScale.x, newScaleY, _wather.transform.localScale.z);
                _oil.transform.localScale = new Vector3(_oil.transform.localScale.x, newScaleY + 0.00015f, _oil.transform.localScale.z);

            }
            else
            {
                _currentMl = _potionMaxMl;
            }
        }
    }

    public void StartingPouringWater(bool status, float velocity)
    {
        _velocityToPouring = velocity;
        _isPouringWater = status;
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
            littleLabelToStick.parent = this.transform;
            switch (_itemName)
            {
                case "Potion Of Wisdom": _itemName = "Potion Of " + label.LittleLabelName; littleLabelToStick.position = _wisdomPosition.position;
                break;
                case "Potion Of Eternal Youth": _itemName = "Potion Of Eternal " + label.LittleLabelName; littleLabelToStick.position = _youthPosition.position;
                break;
                case "Harmony Elixir": _itemName = label.LittleLabelName + " Elixir"; littleLabelToStick.position = _harmonyPositon.position;
                break;
                case "Elixir Of Mind Control": _itemName = "Elixir Of " + label.LittleLabelName + " Control"; littleLabelToStick.position = _mindPosition.position;
                break;
            }
        }
    }

    public void DyePotion(Dye dye)
    {
        if(!_dyePotion || _potionColor == dye.DyeColor)
        {
            _dye.SetActive(true);
            _dye.GetComponentInChildren<MeshRenderer>().material.color = dye.DyeColor;
            _potionColor = dye.DyeColor;
            _colorName = dye.DyeName;
            _colorQuantity++;
            _dyePotion = true;
        }
    }

    public int ChangeValueItem(int multiplier)
    {
        _itemValue *= multiplier;

        return _itemValue;
    }

    public void StopCooking()
    {
        if (startFireCoroutine != null)
        {
            StopCoroutine(startFireCoroutine);
            startFireCoroutine = null; 
        }
    }

    public void CookPotion(float timeToCook)
    {
        if(_currentMl >= _potionMaxMl && _dyePotion)
        {
            if (startFireCoroutine != null)
            {
                StopCoroutine(startFireCoroutine);
            }

            startFireCoroutine = StartCoroutine(StartFire(timeToCook));
        }

    }

    public void AddOil()
    {
        _oil.SetActive(true);
        _oilAdd = true;
    }

    IEnumerator StartFire(float timeToCook)
    {
        yield return new WaitForSeconds(timeToCook);
        
        if(_oilAdd)
        {
            switch(_colorName)
            {
                case "Blue Dye": _wather.GetComponentInChildren<MeshRenderer>().material = _blueOilMaterial;
                break;
                case "Red Dye": _wather.GetComponentInChildren<MeshRenderer>().material = _redOilMaterial;
                break;
                case "Yellow Dye": _wather.GetComponentInChildren<MeshRenderer>().material = _yellowOilMaterial;
                break;
            }  
        }
        else
            _wather.GetComponentInChildren<MeshRenderer>().material.color = _potionColor;
            

        _stopper.SetActive(true);
        _dye.SetActive(false);
        _oil.SetActive(false);
        startFireCoroutine = null; 

    }



    public bool IsPouring
    {
        get {return _isPouringWater;}
        set {_isPouringWater = value;}
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
